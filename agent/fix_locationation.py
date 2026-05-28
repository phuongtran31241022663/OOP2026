from pathlib import Path

root = Path("OOP2026")
changed = []

replacements = [
    (b"LocationationSelected", b"LocationSelected"),
    (b"Locationation", b"Location"),
]

for path in root.rglob("*.cs"):
    data = path.read_bytes()
    new_data = data
    for old, new in replacements:
        new_data = new_data.replace(old, new)
    if new_data != data:
        path.write_bytes(new_data)
        changed.append(str(path))

print("\n".join(changed))