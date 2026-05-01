# TODO: Replace LocationPickerControl with TextBox + ListBox Pattern

## Task
Replace the current ComboBox-based `LocationPickerControl` with a TextBox + ListBox pattern for location search.

## Steps
1. [ ] Modify LocationPickerControl.cs to use TextBox + ListBox pattern
    - [ ] Add TextBox `_txtSearch` for keyword input
    - [ ] Add ListBox `_lstSuggestions` for search results
    - [ ] Wire up search events (TextChanged with debounce)
    - [ ] Handle ListBox selection
2. [ ] Update LocationPickerControl.Designer.cs for new controls
3. [ ] Test the UI in UcPassenger

## Files to Edit
- Presentation/Components/LocationPickerControl.cs
- Presentation/Components/LocationPickerControl.Designer.cs
