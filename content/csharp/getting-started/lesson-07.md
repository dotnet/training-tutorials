# Implementing Logical Expressions
by [Steve Smith](http://deviq.com/me/steve-smith)

## Logical Operators

Logical *expressions* are composed of *operators* and *operands*. You can define your own operators in C#, but that's a more advanced topic. C# provides many built-in operators that you can use to create logical expressions, which are useful when implementing conditional statements, as you saw in the [previous lesson](lesson-06.md). Logical expressions evaluate to ``true`` or ``false``, and as such they are often called ``boolean`` expressions, since they are evaluated as ``bool`` types (and can be assigned to boolean variables).

### Comparison Operators

You've already seen several comparison operators. Below are many of the built-in operators:

```c#
==  // equal
!=  // not equal
>   // greater than
<   // less than
>=  // greater than or equal
<=  // less than or equal
```

Each of the comparison operators requires two operands, one on each side of the expression. For example:

```c#
x < 10
y >= 0
```

To test if a value is between two numbers, you need to check the two conditions separately. The following is not a legal C# expression:

```c#
1 <= x <= 10 // x between 1 and 10 inclusive - DOES NOT COMPILE
```

To combine multiple expression, you use logical operators.

### Conditional Logical Operators

Conditional logical operators are used to combine multiple logical expressions. The most common logical operators are *and*, *or*, and *not*, which are represented as follows:

```c#
&&  // logical AND
||  // logical OR
!   // logical NOT (often read as 'bang')
^   // logical XOR (exclusive OR)
```

The ``&&``, ``||``, and ``^`` operators require two operands; the ``!`` operator takes only one, and is applied as a prefix. For example:

```c#
true && true    // true
true && false   // false
false && false  // false

true || true    // true
true || false   // true
false || false  // false

true ^ true     // false
true ^ false    // true
false ^ false   // false

!true           // false
!false          // true
```

**Note:** C# also includes *bitwise* logical operators, ``&`` (AND), ``|`` (OR). These are used to perform binary comparisons of numeric values, and generally aren't used directly for conditional expressions. The ``^`` (XOR) operator can be used with both boolean and integral operands.

Logical operations are applied left to right, and will *short-circuit*. That is, if the left operand of an ``&&`` operator is false, the right operand will not be evaluated. This is often important, since the operands themselves may be method calls, not variables. You'll learn more about methods in [lesson 12](lesson-12.md).

Logical expressions are grouped using parentheses, which modify their order of operations just as in algebra. For example:

```c#
int a = 5;
int b = 10;
if((a < b) && (b < 20))
{
    // do something
}
```

Be careful with parentheses in logical expression - it's easy to forget to close one. It can be a good practice, especially when you're getting started, to type all of the pairs of parentheses first, and then fill in the values and expressions.

### Flags

A boolean (``bool``) variable is often referred to as a *flag*. Flags can be useful as a means giving a name to a particular condition. For example:

```c#
int x = 10;
bool isPositive = x > 0;
if(isPositive)
{
    // do something
}
```

Often, programs will include complex conditional logic, and it can be helpful to simplify some or all of this complexity into named variables. Flags can be attached to objects as *properties*, such that you can test for their value as part of the object itself (example: ``if(x.IsPositive)``. However, when writing *object-oriented* programs, it's often better to avoid using flags, since they can lead to program designs that are more procedural and don't encapsulate behavior effectively within objects. You'll learn more about object design in [lesson 15](lesson-15.md).

It's a typical convention to name boolean variables with an "Is" or "is" prefix, since this makes it clear the variable is a boolean and also makes reading conditional statements more clear as well. Avoid naming flags negatively (example: "IsNotPositive"), since this can become confusing, especially when the flag is negated with the ``!`` operator.

## Next Steps

Write a program that greets the user using the appropriate greeting for the time of day. Use only ``if``, not ``else`` or ``switch``, statements to do so. Be sure to include the following greetings:

- "Good Morning"
- "Good Afternoon"
- "Good Evening"
- "Good Night"

It's up to you which times should serve as the starting and ending ranges for each of the greetings. If you need a refresher on how to get the current time, see [Working with Dates and Times](lesson-05.md). When testing your program, you'll probably want to use a ``DateTime`` variable you define, rather than the current time. Once you're confident the program works correctly, you can substitute ``DateTime.Now`` for your variable.
