"""Offline checks for the generated, READ-only HTTP surface."""

import json
import re
import unittest
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]
CATALOG = ROOT / "shared/SapB1.Shared.Db/catalog.generated.json"
CONTROLLERS = ROOT / "src/presentation/Controllers"


class ApiContractTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.catalog = json.loads(CATALOG.read_text(encoding="utf-8"))

    def test_every_category_has_an_authorized_controller(self):
        controllers = list((CONTROLLERS / "Categories").glob("*TablesController.cs"))
        self.assertEqual(12, len(controllers))
        policies = set()
        for path in controllers:
            source = path.read_text(encoding="utf-8")
            match = re.search(r'\[Authorize\(Policy = "([^"]+)"\)\]', source)
            self.assertIsNotNone(match, path.name)
            policies.add(match.group(1))
            self.assertIn('[HttpGet("{code}/rows")]', source)
            self.assertNotRegex(source, r"\[Http(Post|Put|Patch|Delete)")
        self.assertEqual(12, len(policies))
        catalog_controller = (CONTROLLERS / "CatalogController.cs").read_text(encoding="utf-8")
        self.assertIn('[Authorize(Policy = "sap-b1.catalog.read")]', catalog_controller)

    def test_catalog_and_reader_coverage(self):
        tables = self.catalog["tables"]
        self.assertEqual(2709, len(tables))
        mapped = {table["code"] for table in tables if table["status"] == "mapped"}
        self.assertEqual(2547, len(mapped))
        source = "\n".join(path.read_text(encoding="utf-8")
                           for path in (ROOT / "src/services/Readers").glob("*Readers.cs"))
        registered = set(re.findall(r'readers.Add\("([^"]+)"', source))
        self.assertEqual(mapped, registered)

    def test_default_is_deny_and_no_generated_cs_suffix(self):
        config = json.loads((ROOT / "src/presentation/appsettings.json").read_text(encoding="utf-8"))
        self.assertEqual({}, config["SapB1"]["Tables"])
        service = (ROOT / "src/services/SapTableService.cs").read_text(encoding="utf-8")
        self.assertIn("explicit column allowlist", service)
        self.assertFalse([path for path in ROOT.rglob("*.g.cs")
                          if not {"obj", "bin"}.intersection(path.parts)])


if __name__ == "__main__":
    unittest.main()
