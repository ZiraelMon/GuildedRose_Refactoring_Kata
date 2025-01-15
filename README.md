# Gilded Rose Refactoring Kata - C# Implementation

## AKINF: Developing Sustainable Software - Group 2

## Assignment Overview

This project is a refactoring exercise focused on improving the code quality of the Gilded Rose inventory management system. The main objectives were:

1. Understanding the existing codebase
2. Writing comprehensive tests
3. Refactoring the code while maintaining functionality
4. Improving code maintainability and readability

## Project Structure

- `GildedRose/` - Main implementation
  - `GildedRose.cs` - Core business logic
  - `Item.cs` - Item class definition
  - `Program.cs` - Console application entry point
- `GildedRoseTests/` - Test suite
  - `GildedRoseTest.cs` - Unit tests
  - `ApprovalTest.cs` - Approval tests for verification

## Key Improvements

### 1. Code Organization
- Separated item update logic into specific methods
- Removed nested conditional statements
- Added constants for magic strings and numbers
- Improved method naming for clarity

### 2. Testing
- Comprehensive test suite covering all business rules
- Edge case testing
- Boundary value testing
- Special item behavior verification

### 3. Maintainability
- Single Responsibility Principle applied
- Clear method structure
- Reduced code duplication
- Improved error handling

## Building and Running

### Build the Project
```cmd
dotnet build GildedRose.sln -c Debug
```

### Run the Program
For example, to simulate 10 days:
```cmd
GildedRose/bin/Debug/net8.0/GildedRose 10
```

### Run Tests
```cmd
dotnet test
```

## Business Rules

The system follows these core rules:

1. All items have a SellIn value (days remaining) and a Quality value
2. Quality decreases by 1 each day
3. Once the sell by date passes, Quality degrades twice as fast
4. Quality is never negative
5. "Aged Brie" increases in Quality over time
6. Quality never exceeds 50
7. "Sulfuras" never changes
8. "Backstage passes" increase in Quality as SellIn approaches:
   - Quality increases by 2 when there are 10 days or less
   - Quality increases by 3 when there are 5 days or less
   - Quality drops to 0 after the concert


## Contributing

Feel free to submit issues and enhancement requests!
