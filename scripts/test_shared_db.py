"""Source-backed coverage checks for the generated shared DB library."""

import json
from pathlib import Path
import re
import sys
import unittest


ROOT = Path(__file__).resolve().parents[1]
DB = ROOT / 'shared/SapB1.Shared.Db'
sys.path.insert(0, str(ROOT / 'scripts'))
from generate_shared_db import enum_options


class SharedDbTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.catalog = json.loads((DB / 'catalog.generated.json').read_text(encoding='utf-8'))

    def test_category_and_page_coverage(self):
        catalog = self.catalog
        self.assertEqual(12, len(catalog['categories']))
        self.assertEqual(2709, catalog['summary']['catalogTables'])
        self.assertEqual(2547, catalog['summary']['mappedTables'])
        self.assertEqual(162, catalog['summary']['missingLocalPages'])
        self.assertEqual(91281, catalog['summary']['mappedFields'])
        self.assertEqual(2709, len({table['code'] for table in catalog['tables']}))
        self.assertEqual(162, len(catalog['missingTableCodes']))
        self.assertEqual(12, len(list(DB.glob('*/*DbContext.cs'))))
        self.assertEqual(2547, len(list(DB.glob('*/Domain/*.cs'))))
        self.assertEqual([], [path for category in catalog['categories']
                              for path in (DB / category['namespace']).rglob('*.g.cs')])

    def test_only_documented_tables_have_queryable_domain(self):
        for category in self.catalog['categories']:
            context = (DB / category['namespace'] /
                       f"{category['namespace']}DbContext.cs").read_text(encoding='utf-8')
            for table in (t for t in self.catalog['tables'] if t['category'] == category['name']):
                if table['status'] == 'mapped':
                    domain = DB / category['namespace'] / 'Domain' / f"{table['className']}.cs"
                    self.assertTrue(domain.exists(), table['code'])
                    self.assertIn(f'[Table(Name = "{table["code"]}")]', domain.read_text(encoding='utf-8'))
                    self.assertIn(f'ITable<{table["className"]}>', context)
                else:
                    self.assertIn(table['code'], self.catalog['missingTableCodes'])
                    self.assertNotIn('className', table)

    def test_item_master_keeps_sql_identifiers_and_no_inferred_key(self):
        table = next(t for t in self.catalog['tables'] if t['code'] == 'OITM')
        self.assertEqual('Inventory and Production', table['category'])
        self.assertEqual(330, table['fieldCount'])
        self.assertEqual('ItemsRow', table['className'])
        source_columns = {f['column']: f for f in table['fields']}
        self.assertEqual('ItemNo', source_columns['ItemCode']['property'])
        self.assertEqual('ItemGroup', source_columns['ItmsGrpCod']['property'])
        domain = (DB / 'InventoryAndProduction/Domain/ItemsRow.cs').read_text(encoding='utf-8')
        self.assertIn('[Column(Name = "ItemCode")]', domain)
        self.assertNotIn('[PrimaryKey', domain)
        self.assertFalse(re.search(r'\bU_[A-Za-z0-9_]+\b', domain))

    def test_administration_name_enum_metadata_and_diagram(self):
        table = next(t for t in self.catalog['tables'] if t['code'] == 'AADM')
        self.assertEqual('AdministrationLogRow', table['className'])
        field = next(f for f in table['fields'] if f['column'] == 'DispPosDeb')
        self.assertEqual('OpenBalanceWithMinusSign', field['property'])
        self.assertEqual([{'code': 'Y', 'label': 'Yes'}, {'code': 'N', 'label': 'No'}],
                         field['enumOptions'])
        domain = (DB / 'Administration/Domain/AdministrationLogRow.cs').read_text(encoding='utf-8')
        enum = (DB / 'Administration/Enums/AdministrationEnums.cs').read_text(encoding='utf-8')
        self.assertIn('[Description("Company Name")]', domain)
        self.assertIn('AdministrationLogOpenBalanceWithMinusSignOption?', domain)
        self.assertIn('[MapValue("Y")]', enum)
        self.assertTrue((DB / table['sampleSelectPath']).read_text(encoding='utf-8').lstrip().startswith('SELECT'))
        graph = json.loads((DB / 'relationship-map.generated.json').read_text(encoding='utf-8'))
        self.assertEqual(1712, graph['diagrams'])
        self.assertEqual(['PMN5'], graph['diagramFieldMismatches'])
        aadm = next(t for t in graph['tables'] if t['table'] == 'AADM')
        self.assertTrue(aadm['diagram']['present'])
        self.assertTrue(any(e['fromTable'] == 'AADM' for e in aadm['diagram']['edges']))

    def test_enum_requires_complete_code_label_pairs(self):
        self.assertEqual([{'code': 'Y', 'label': 'Yes'}, {'code': 'N', 'label': 'No'}],
                         enum_options('Y)  Yes)  N)  No', 'VarChar', '1'))
        self.assertEqual([], enum_options('Y)  N', 'VarChar', '1'))
        self.assertEqual([], enum_options('Y)  Yes)  N)  No', 'Num', '19'))


if __name__ == '__main__':
    unittest.main()
