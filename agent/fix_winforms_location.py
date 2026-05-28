from pathlib import Path

root = Path("OOP2026")
changed = []

for path in root.rglob("*.cs"):
    data = path.read_bytes()
    new_data = data.replace(b".Loc", b".Location")
    if new_data != data:
        path.write_bytes(new_data)
        changed.append(str(path))

print("\n".join(changed))