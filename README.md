# Enterprise Application Framework (c. 2004-2008 for .NET 1.1/2.0)

### **Purpose of This Repository**

This source code is preserved as a **historical portfolio piece.** It is intended to demonstrate foundational architectural skills and the ability to build a complex, platform-level solution from first principles.

The code was written at a time when many of the features now standard in modern .NET (like Entity Framework, ASP.NET Identity, robust MVC frameworks, and client-side build tools) did not yet exist. As such, it represents a complete, in-house solution to the challenges of enterprise application development in that era.

**This project is not intended for modern production use.** Its value lies in the architectural patterns and the scope of the solution.

### Project Context

This framework was originally developed under the name `IRSA.Framework` while I was working as an in-house developer at the real estate corporation IRSA. It was born from a practical need to accelerate and standardize application development across the company. I architected and built it primarily in my free time.

The framework was so successful at reducing development time and improving code quality that it became the company-wide standard for all new .NET applications and was used by the development team for years after my departure.

### Key Architectural Features

This framework was designed to be a complete, end-to-end "solution stack" and includes:

*   **A Database-Agnostic Data Access Layer:** A full provider model for SQL Server, MySQL, and ODBC, serving as a home-grown ORM and data access solution.
*   **A Generic Repository Pattern:** A set of base classes for business logic and data access (`BaseBLData`, `BaseDAData`) that promoted code reuse and a clean separation of concerns.
*   **An Enterprise Security Framework:** A comprehensive, role-based security system with a provider model for different backends (SQL, Active Directory), declarative security via custom attributes (`[RequiresPermission]`), and a full object model for users, groups, and permissions.
*   **An Aspect-Oriented Programming (AOP) Framework:** A custom interception model (`IRSA.Framework.Interception`) for handling cross-cutting concerns like logging and security.
*   **A Full Web Framework (MVC-like):**
    *   A component and templating model for ASP.NET that could auto-generate complex UI, including full CRUD (ABM) forms from a data object.
    *   A complete, custom-built JavaScript UI widget library (`CollapsableTable`, `DateSelector`, `TabContainer`, etc.).
    *   An AJAX communication layer with a connection manager, built from scratch before libraries like jQuery made this common.
*   **A Server-Side JavaScript Pre-processor:** The framework included a custom IIS handler that would parse `.pjs` files, treating them as templates. This engine injected server-side configuration values (like database connection strings or web service endpoints) directly into the client-side JavaScript, solving a critical configuration challenge of the era.
*   **A Background Job/Queueing System:** A "Spooler" (`IRSA.Framework.Spooler`) for managing and executing asynchronous background jobs.

### A Note on the Technology

This code is written for the .NET Framework 1.1/2.0 and is not directly compatible with modern .NET (Core / 8+). It serves as a testament to the timelessness of good architectural patterns. The solutions implemented here—from the provider model to the component-based UI—are the very same problems that modern frameworks solve today, just with a different set of tools.
