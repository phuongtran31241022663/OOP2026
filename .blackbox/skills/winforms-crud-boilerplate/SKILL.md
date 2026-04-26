---
name: winforms-crud-boilerplate
description: Generate a complete WinForms CRUD form with DataGridView, add/edit/delete/save/cancel buttons, and in-memory data binding. Use this when user asks to create a data management form, a CRUD UI, or an admin panel for any entity.
---

# WinForms CRUD Boilerplate

## Instructions

Given an entity class name and its properties, generate a full WinForms form that supports Create, Read, Update, Delete operations using a `BindingSource` and a `List<T>` as data store.

### Steps to follow

1. **Ask for entity details** – If not provided, request:
   - Entity class name (e.g., `Customer`, `Product`)
   - List of public properties with types (e.g., `int Id`, `string Name`, `decimal Price`)
   - Any specific validation rules (optional)

2. **Generate the entity class** (if not exists) – simple POCO with auto-properties.

3. **Generate the form code**:
   - `DataGridView` docked to top or fill, with auto-generated columns.
   - `BindingNavigator` or a `Panel` with buttons: Add, Edit, Delete, Save, Cancel.
   - `BindingSource` bound to a `List<T>`.
   - Form load: populate with sample data (optional) or empty list.
   - Add/Edit: open a small dialog with `PropertyGrid` or dynamically created `TextBox` controls for each property (skip Id if auto-increment).
   - Delete: confirm before removal.
   - Save: for demo purposes, display a message or save to JSON file (ask user preference).

4. **Provide complete code** – include `using` statements, event handlers, and comments explaining key parts.

### Example

**User input:**

> “Create a CRUD form for `Book` with properties: `int Id`, `string Title`, `string Author`, `double Price`, `int Year`.”

**Output:** Full `Book.cs` and `BookManagementForm.cs` with all CRUD operations, a simple add/edit dialog, and a confirmation before delete. The form loads with 2–3 sample books.

### Notes

- Use `System.Windows.Forms` and `System.ComponentModel`.
- For property editing, use `PropertyGrid` for simplicity.
- Alternatively, generate a second small form with `TextBox` controls for each property.
- Id handling: generate new Id as `max+1` for new items.
- For saving to file, offer to save as JSON using `Newtonsoft.Json` or `System.Text.Json`.

### Best practices

- Keep the form self-contained; no external dependencies beyond .NET Framework/WinForms.
- Use `List<T>` as the data store for simplicity.
- Provide error handling (e.g., invalid price or year).
- Offer an option to export data to CSV/JSON if user requests.
