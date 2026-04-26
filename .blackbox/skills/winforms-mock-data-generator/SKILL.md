---
name: winforms-mock-data-generator
description: Generate realistic mock data for any C# class or list, using Bogus or manual randomization. Use when user needs sample data for testing grids, charts, reports, or demos.
---

# WinForms Mock Data Generator

## Instructions

Given a C# type (class name with properties), generate code that creates a collection of mock objects populated with plausible random values.

### Approach

Prefer using the **Bogus** library for realistic data (names, addresses, phone numbers, etc.). If Bogus is not available or user requests lightweight, fall back to manual `Random` generation.

### Steps

1. **Identify the target type** – ask user for:
   - Class name and full definition (if new, ask for properties).
   - Number of items to generate (default 10).
   - Any specific constraints (e.g., “Price between 10 and 100”, “Date in 2024”).

2. **Generate the mock data code**:
   - If using Bogus: create a `Faker<T>` rule set.
   - If manual: create a loop with `Random` and assign values.

3. **Provide usage examples** – how to bind the generated list to `DataGridView.DataSource`, or how to use it in a unit test.

4. **Offer variations** – sequential IDs, random enums, nested objects, or lists inside objects.

### Examples

#### Example 1: Mock for `Customer` class

**User input:**  
Class:

```csharp
public class Customer {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}
```
