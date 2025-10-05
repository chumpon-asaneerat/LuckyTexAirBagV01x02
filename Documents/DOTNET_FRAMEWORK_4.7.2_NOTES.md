# .NET Framework 4.7.2 - Compatibility Reference

## [!] CRITICAL CONSTRAINT

This project **MUST remain on .NET Framework 4.7.2**. There is NO migration to .NET Core or .NET 5+.

---

## Overview

### What is .NET Framework 4.7.2?

**Release Date**: April 2018
**End of Support**: Extended support until at least 2029 (Windows 10 lifecycle)
**Maximum C# Version**: 7.3 (officially supported)

### Why Stay on .NET Framework?

- **Proprietary dependencies**: NLib Framework (proprietary, .NET Framework only)
- **Windows-only deployment**: No need for cross-platform
- **Stability**: Proven, stable codebase
- **WPF**: Full WPF support (WPF in .NET Core/5+ still has limitations)
- **Oracle Client**: Existing Oracle.DataAccess works well

---

## C# Language Features

### Available (C# 7.3 and earlier)

#### C# 7.3 Features
- [x] **Tuples** - ValueTuple, tuple deconstruction
- [x] **Pattern matching** - is pattern, switch pattern
- [x] **Ref returns and locals** - ref local variables, ref returns
- [x] **Local functions** - functions inside methods
- [x] **Out variables** - inline out variable declarations
- [x] **Digit separators** - numeric literal improvements (1_000_000)
- [x] **Binary literals** - 0b prefix for binary
- [x] **Expression-bodied members** - properties, constructors, finalizers
- [x] **Throw expressions** - throw in expression context
- [x] **Generalized async return types** - ValueTask<T>
- [x] **Default literal expressions** - default keyword

#### C# 7.2 Features
- [x] **Ref readonly** - readonly references
- [x] **In parameters** - readonly ref parameters
- [x] **Private protected** access modifier
- [x] **Conditional ref expressions** - ref ternary operator
- [x] **Leading underscores** in numeric literals

#### C# 7.1 Features
- [x] **Async Main** - async entry point
- [x] **Default expressions** - default literal
- [x] **Inferred tuple names** - automatic tuple element names
- [x] **Pattern matching with generics**

#### C# 7.0 Features
- [x] **Out variables** - declare inline
- [x] **Tuples** - ValueTuple support
- [x] **Pattern matching** - is and switch statements
- [x] **Local functions**
- [x] **More expression-bodied members**
- [x] **Ref locals and returns**
- [x] **Discards** - _ placeholder
- [x] **Throw expressions**
- [x] **Deconstruction**

#### C# 6.0 and Earlier
- [x] **Null-conditional operators** - ?. and ?[]
- [x] **String interpolation** - $"Hello {name}"
- [x] **Expression-bodied methods** - => syntax
- [x] **Auto-property initializers** - public string Name { get; set; } = "Default";
- [x] **nameof expressions** - nameof(variable)
- [x] **Index initializers** - dictionary[key] = value in initializer
- [x] **Exception filters** - catch (Exception ex) when (condition)
- [x] **await in catch/finally** blocks
- [x] **async/await** (C# 5.0)
- [x] **LINQ** (C# 3.0)
- [x] **Lambda expressions** (C# 3.0)
- [x] **Extension methods** (C# 3.0)
- [x] **Anonymous types** (C# 3.0)

### NOT Available (C# 8.0+)

#### C# 8.0 Features (.NET Core 3.0+ required)
- [ ] **Nullable reference types** - string? nullable annotation
- [ ] **Async streams** - IAsyncEnumerable<T>
- [ ] **Ranges and indices** - array[^1], array[1..5]
- [ ] **Default interface methods** - interface with implementation
- [ ] **Using declarations** - using var without braces
- [ ] **Static local functions**
- [ ] **Switch expressions** - improved switch
- [ ] **Property patterns** - enhanced pattern matching
- [ ] **Tuple patterns**
- [ ] **Positional patterns**

#### C# 9.0 Features (.NET 5+ required)
- [ ] **Records** - record types
- [ ] **Init-only setters** - init keyword
- [ ] **Top-level statements**
- [ ] **Pattern matching enhancements**
- [ ] **Target-typed new** - new() without type
- [ ] **Covariant returns**
- [ ] **Extension GetEnumerator**
- [ ] **Lambda discard parameters**
- [ ] **Attributes on local functions**

#### C# 10.0+ Features (.NET 6+ required)
- [ ] **Global using directives**
- [ ] **File-scoped namespaces**
- [ ] **Extended property patterns**
- [ ] **Record structs**
- [ ] **Interpolated string handlers**

---

## .NET Libraries and Features

### Available in .NET Framework 4.7.2

#### Core Libraries
- [x] **System.Collections.Generic** - List<T>, Dictionary<T>, etc.
- [x] **System.Linq** - LINQ queries and methods
- [x] **System.Threading.Tasks** - Task, Task<T>, async/await
- [x] **System.Net.Http** - HttpClient (full support)
- [x] **System.Text.RegularExpressions** - Regex
- [x] **System.Xml** - XML processing
- [x] **System.Reflection** - Reflection APIs
- [x] **System.ComponentModel.DataAnnotations** - Validation attributes

#### WPF
- [x] **Full WPF support** - System.Windows namespace
- [x] **XAML** - Complete XAML support
- [x] **Data binding** - INotifyPropertyChanged, etc.
- [x] **Commands** - ICommand
- [x] **Styles and templates**
- [x] **Dependency properties**

#### Database
- [x] **System.Data.SqlClient** - SQL Server
- [x] **System.Data.OracleClient** - Oracle (deprecated but works)
- [x] **Oracle.DataAccess** - Oracle ODP.NET (third-party)
- [x] **Entity Framework 6.x** - Full EF6 support
- [x] **ADO.NET** - DataTable, DataSet, etc.

#### Async/Await
- [x] **Task-based async** - async/await fully supported
- [x] **ConfigureAwait** - for context control
- [x] **Task.Run** - offload to thread pool
- [x] **ValueTask<T>** - C# 7.0+ (via NuGet)

### NOT Available (Requires .NET Core/.NET 5+)

#### Modern Types
- [ ] **Span<T>** - stack-allocated memory slices
- [ ] **Memory<T>** - managed memory slices
- [ ] **ReadOnlySpan<T>** - readonly memory slices
- [ ] **ArrayPool<T>** - (available via NuGet System.Buffers but limited)

#### Modern APIs
- [ ] **System.Text.Json** - modern JSON serializer (use Newtonsoft.Json instead)
- [ ] **IAsyncEnumerable<T>** - async streams
- [ ] **Index and Range** - ^, .. operators (language support missing)
- [ ] **System.IO.Pipelines** - high-performance I/O

#### Modern Framework Features
- [ ] **Host/Generic Host** - ASP.NET Core hosting
- [ ] **Built-in DI** - Microsoft.Extensions.DependencyInjection (can add via NuGet)
- [ ] **Configuration system** - Microsoft.Extensions.Configuration (can add via NuGet)
- [ ] **Logging abstraction** - Microsoft.Extensions.Logging (can add via NuGet)

---

## Recommended Libraries (Compatible)

### Dependency Injection

#### Simple Injector (Recommended)
- **Version**: 4.x (latest compatible with .NET Framework 4.7.2)
- **Why**: Lightweight, fast, simple API
- **NuGet**: `Install-Package SimpleInjector`
- **WPF Integration**: `Install-Package SimpleInjector.Integration.Wpf`

#### Unity Container
- **Version**: 5.x
- **Why**: Feature-rich, Microsoft-backed
- **NuGet**: `Install-Package Unity`

#### Autofac
- **Version**: 4.x-6.x
- **Why**: Powerful, flexible
- **NuGet**: `Install-Package Autofac`

### Validation

#### FluentValidation
- **Version**: 8.x-10.x (check .NET Framework 4.7.2 compatibility)
- **Why**: Expressive, testable validation
- **NuGet**: `Install-Package FluentValidation`

### Object Mapping

#### AutoMapper
- **Version**: 10.x (last version supporting .NET Framework 4.7.2)
- **Why**: Automatic object-to-object mapping
- **NuGet**: `Install-Package AutoMapper`

### JSON

#### Newtonsoft.Json (Json.NET)
- **Version**: 13.x (latest)
- **Why**: De facto JSON library for .NET Framework
- **NuGet**: `Install-Package Newtonsoft.Json`
- **Note**: Do NOT use System.Text.Json (not available)

### Logging

#### NLog
- **Version**: 4.x-5.x
- **Why**: Mature, flexible, well-documented
- **NuGet**: `Install-Package NLog`

#### Serilog
- **Version**: 2.x
- **Why**: Structured logging, modern API
- **NuGet**: `Install-Package Serilog`

### Testing

#### NUnit
- **Version**: 3.x
- **Why**: Popular, well-supported
- **NuGet**: `Install-Package NUnit`, `Install-Package NUnit3TestAdapter`

#### xUnit
- **Version**: 2.x
- **Why**: Modern, flexible
- **NuGet**: `Install-Package xunit`, `Install-Package xunit.runner.visualstudio`

#### Moq (Mocking)
- **Version**: 4.x
- **Why**: Easy mocking for unit tests
- **NuGet**: `Install-Package Moq`

### Data Access

#### Dapper (Micro-ORM)
- **Version**: 2.x
- **Why**: Fast, simple object mapping for ADO.NET
- **NuGet**: `Install-Package Dapper`

#### Entity Framework 6
- **Version**: 6.x (NOT EF Core)
- **Why**: Full-featured ORM for .NET Framework
- **NuGet**: `Install-Package EntityFramework`

### Resilience

#### Polly
- **Version**: 7.x
- **Why**: Retry, circuit breaker, timeout policies
- **NuGet**: `Install-Package Polly`

---

## Common Pitfalls and Solutions

### Pitfall 1: Using System.Text.Json

**Problem**: System.Text.Json doesn't exist in .NET Framework 4.7.2

**Wrong**:
```csharp
using System.Text.Json;
var json = JsonSerializer.Serialize(obj); // Compile error
```

**Correct**:
```csharp
using Newtonsoft.Json;
var json = JsonConvert.SerializeObject(obj);
```

### Pitfall 2: Using Span<T>

**Problem**: Span<T> requires .NET Core/5+

**Wrong**:
```csharp
Span<byte> buffer = stackalloc byte[256]; // Compile error
```

**Correct** (alternatives):
```csharp
// Use byte array
byte[] buffer = new byte[256];

// Or ArrayPool for performance
var pool = ArrayPool<byte>.Shared;
byte[] buffer = pool.Rent(256);
try
{
    // Use buffer
}
finally
{
    pool.Return(buffer);
}
```

### Pitfall 3: Using IAsyncEnumerable<T>

**Problem**: IAsyncEnumerable<T> requires C# 8.0+

**Wrong**:
```csharp
async IAsyncEnumerable<int> GetNumbers() // Compile error
{
    for (int i = 0; i < 10; i++)
    {
        await Task.Delay(100);
        yield return i;
    }
}
```

**Correct** (alternatives):
```csharp
// Use Task<IEnumerable<T>>
async Task<IEnumerable<int>> GetNumbers()
{
    var numbers = new List<int>();
    for (int i = 0; i < 10; i++)
    {
        await Task.Delay(100);
        numbers.Add(i);
    }
    return numbers;
}

// Or yield return with IEnumerable<T>
IEnumerable<int> GetNumbers()
{
    for (int i = 0; i < 10; i++)
    {
        yield return i;
    }
}
```

### Pitfall 4: Using Records

**Problem**: Records require C# 9.0+

**Wrong**:
```csharp
public record Person(string Name, int Age); // Compile error
```

**Correct**:
```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    // Can implement IEquatable<T> manually if needed
    public override bool Equals(object obj)
    {
        if (obj is Person other)
            return Name == other.Name && Age == other.Age;
        return false;
    }

    public override int GetHashCode()
    {
        return (Name, Age).GetHashCode();
    }
}
```

### Pitfall 5: Using Init-Only Setters

**Problem**: Init-only setters require C# 9.0+

**Wrong**:
```csharp
public string Name { get; init; } // Compile error
```

**Correct**:
```csharp
// Use private setter if immutability desired
public string Name { get; private set; }

// Or constructor-only initialization
private readonly string _name;
public string Name => _name;

public Person(string name)
{
    _name = name;
}
```

### Pitfall 6: Using Nullable Reference Types

**Problem**: Nullable reference types require C# 8.0+

**Wrong**:
```csharp
#nullable enable
string? nullableName = null; // Compile error (syntax not recognized)
```

**Correct**:
```csharp
// Just use standard null checks
string nullableName = null; // No special syntax
if (nullableName != null)
{
    // Use nullableName
}

// Or null-conditional operators (C# 6.0)
var length = nullableName?.Length ?? 0;
```

---

## Performance Considerations

### What Works Well in .NET Framework 4.7.2

#### Async/Await
- Fully supported and performant
- Use ConfigureAwait(false) in library code
- ValueTask<T> available via NuGet for hot paths

#### LINQ
- Fully optimized
- Use deferred execution wisely
- Consider AsParallel() for CPU-bound operations

#### Collections
- Generic collections are optimized
- Use appropriate collection types (List<T>, Dictionary<T>, HashSet<T>)

#### String Operations
- StringBuilder for concatenation
- String.Format or string interpolation ($"") both fine
- Regex compiled option for reused patterns

### What to Avoid for Performance

#### Boxing
- Avoid boxing value types to object
- Use generic collections instead of ArrayList
- Be careful with LINQ on value types

#### Excessive Allocations
- Reuse objects when possible (object pooling)
- Use StringBuilder for string concatenation in loops
- Consider ArrayPool<T> for temporary buffers

#### Reflection
- Cache MethodInfo, PropertyInfo, etc.
- Use compiled expressions for repeated access
- Consider source generators (if migrating to .NET 5+ later)

---

## Migration Path (Future)

### When to Consider Migration

- **Windows-only requirement ends** - Need cross-platform
- **Proprietary framework replaced** - NLib no longer used
- **Performance critical** - Need Span<T>, Memory<T> for hot paths
- **.NET Framework end of life approaching** - Extended support ending

### Migration Challenges

#### Code Changes Required
- [ ] Replace Newtonsoft.Json with System.Text.Json (API differences)
- [ ] Update to C# 8.0+ (nullable reference types, switch expressions)
- [ ] Replace .NET Framework-specific APIs
- [ ] Test WPF differences (.NET Core/5+ WPF has some gaps)

#### Library Updates Required
- [ ] Simple Injector → version 5.x+
- [ ] AutoMapper → version 11.x+
- [ ] FluentValidation → version 11.x+
- [ ] Oracle Client → Oracle.ManagedDataAccess.Core

#### Testing Required
- [ ] Full regression testing (behavior differences possible)
- [ ] Performance testing (can be faster or slower)
- [ ] WPF rendering differences
- [ ] Third-party library compatibility

### Recommended Migration Strategy (If Needed)

1. **Complete modernization on .NET Framework 4.7.2 first**
   - Get all architectural improvements in place
   - Achieve testability and separation of concerns
   - This makes later migration much easier

2. **Migrate to .NET Framework 4.8** (intermediate step)
   - Still .NET Framework, minimal changes
   - More modern APIs available
   - Better performance

3. **Multi-target .NET Framework 4.8 and .NET 6+**
   - Use conditional compilation for differences
   - Gradual migration approach
   - Test both targets

4. **Drop .NET Framework support when ready**
   - After thorough testing on .NET 6+
   - When all dependencies migrated

---

## Quick Reference

### Can I Use...?

| Feature | Available? | Alternative if No |
|---------|------------|-------------------|
| async/await | [x] Yes | - |
| LINQ | [x] Yes | - |
| Tuples (ValueTuple) | [x] Yes | - |
| Pattern matching (is, switch) | [x] Yes (limited) | - |
| Local functions | [x] Yes | - |
| Span<T> | [ ] No | byte[], ArrayPool<T> |
| System.Text.Json | [ ] No | Newtonsoft.Json |
| IAsyncEnumerable<T> | [ ] No | Task<IEnumerable<T>> |
| Records | [ ] No | Classes with Equals/GetHashCode |
| Init-only setters | [ ] No | Private setters, readonly fields |
| Nullable reference types | [ ] No | Standard null checks |
| Ranges/indices (^, ..) | [ ] No | Manual indexing |
| Switch expressions | [ ] No | Switch statements |
| Default interface methods | [ ] No | Abstract classes |
| Entity Framework | [x] Yes (EF6) | NOT EF Core |
| Dependency Injection | [x] Yes | Simple Injector, Unity, Autofac |
| HttpClient | [x] Yes | - |
| Task-based async | [x] Yes | - |

---

## Resources

### Official Documentation
- **.NET Framework 4.7.2 Release Notes**: https://docs.microsoft.com/en-us/dotnet/framework/whats-new/
- **C# 7.3 Features**: https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-3
- **WPF Documentation**: https://docs.microsoft.com/en-us/dotnet/desktop/wpf/

### Library Documentation
- **Simple Injector**: https://simpleinjector.org/
- **Newtonsoft.Json**: https://www.newtonsoft.com/json/help/html/Introduction.htm
- **FluentValidation**: https://docs.fluentvalidation.net/
- **AutoMapper**: https://docs.automapper.org/
- **NLog**: https://nlog-project.org/documentation/
- **Serilog**: https://serilog.net/

### Community Resources
- **Stack Overflow**: Tag [.net-framework-4.7.2]
- **GitHub**: Search for .NET Framework 4.7.2 compatible libraries

---

**Document Version**: 1.0
**Last Updated**: 2025-10-05
**.NET Framework Version**: 4.7.2
**C# Maximum Version**: 7.3
