# When and How to use Exceptions
by [Brendan Enrick](http://deviq.com/me/brendan-enrick)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## Basics of Exceptions
In programming, *exceptions* are errors our programs throw in response to circumstances that are not intended and require special processing. Most often these are when an error has ocurred and needs to be handled by your program. In previous lessons, you've seen places where code you are calling throws an exception. One exception that you may have tried already happens when you typed in anything that wasn't a number during the guessing game. Doing this would result in ``Unhandled Exception: System.FormatException: Input string was not in a correct format.`` being displayed and your program would crash.

```c#
public static void Main(string[] args)
{
    try
    {
        int sum = SumNumberStrings(new List<string> {"5", "4"});
        Console.WriteLine(sum);
    }
    catch (System.FormatException)
    {
        Console.WriteLine("List of numbers contained an invalid entry.");
    } 
}

public static int SumNumberStrings(List<string> numbers)
{
    int total = 0;
    foreach (string numberString in numbers)
    {
        total += int.Parse(numberString);
    }
    return total;
}
```

### Catching Exceptions
When code you write has the potential to throw an exception, it's important that the exception be caught and handled by something. You do this, by using a *try-catch* block in your code. This is an empty example of one:

```c#
try
{
    // code that may throw an exception here
}
catch (System.Exception ex)
{
    // handle the exception here
}
```

The first block of code, called a *try* block, is where you put all of your code that may throw an exception. If an exception is thrown by your code, your program will skip any remaining work and jump directly to your *catch* block, the second block of code in the example. It's at this point that you are able to handle the exception. You should also notice the ``(System.Exception ex)`` variable declaration, which gives you access to the details of the exception during the catch block by using the ``ex`` variable.

When an exception ocurrs, your code will need to decide if it should continue running or whether it has no solution to the issue. If you have a solution to the issue, your code can correct the state of things to allow you to continue. If your code cannot resolve the error, you will want to make sure that you re-throw the exception. To do this, you want to use the ``throw`` statement on its own line without giving it an exception. 

**Note:** If your code isn't going to respond to or log the exception in any way, don't catch the exception in the first place.

Throwing a new exception will replace the previous exception, so only do this if you're using an exception specific to your application (and make sure to set the ``InnerException`` property of your new exception to the previous exception.

**Note:** It is common to log exceptions that ocurr, so that you can review the code later to make sure you're handling the exception as best you can.

### Throwing Exceptions
When you're in one fo those unexpected situations and your code cannot (or should not) handle this situation itself, it's time to throw an exception.

```c#
public List<People> GetAllCustomersByAge(int age)
{
    if (age < 18 || age > 150)
    {
        throw new ArgumentOutOfRangeException("Age must be between 18 and 150.", "age");
    }
}
```

When throwing exceptions, you use the ``throw`` keyword to indicate that you're throwing an exception followed by the ``Exception`` object you're throwing. In the example above, the exception object is being created on the same line on which it's thrown. Also notice that a specific type was used here, the ``ArgumentOutOfRangeException``. This means that anyone catching the exception knows that an argument recevied was not within the expected range. Some exception types, like the ``ArgumentOutOfRangeException``, take additional arguments in their constructors. For the various exceptions that derive from the ``System.ArgumentException``, like this example, the constructor takes an error message and the name of the argument containing the invalid value.

**Note:** Never throw ``System.Exception``, ``System.SystemException``, or ``ApplicationException`` directly; use more specific standard exceptions or custom exceptions. This allows callers to choose which exceptions they can handle instead of needing to respond to all exceptions.

### The Finally Block
In your code, you may need to guarantee that you've tied up all loose ends, whether you threw an exception or not. In these circumstances, the *finally* block is what you're looking for. 

```c#
try
{
}
catch (System.Exception ex)
{
}
finally
{
    // This code will always run
    // even if your catch block re-throws
}
// Code here will run only if catch doesn't re-throw
```

Most people ask why they need to use a finally block, since the code after the try-catch runs anyway. If the catch block handles the exception and allows your code to keep running, it will be functionally equivalent. If the catch block re-throws or throws a new exception (intentionally or not), your finally will still run. This means you can depend on the finally block executing, but be careful to not throw an exception in the finally block.

It is also possible to use a *try-finally* block if you want the guarantee, but aren't going to be handling the exception.

```c#
try
{
}
finally
{
    // This code will always run - even after exceptions
}
// Code here will run only if no exceptions are thrown
```

### Throwing From Catch Blocks
In your catch blocks, you can put any code. This includes throwing exceptions, or calling other code that could throw an exception. If an exception is thrown from inside of your catch block, the details of the original exception will be lost, so it's important to use caution. If you want to catch a low-level exception and throw one more relevant to your program, make sure to assign the low-level one to the ``InnerException`` property of the new exception. This will keep the information available should an investigation be necessary.

```c#
try
{
}
catch (ArgumentNullException ex)
{
    throw new UserRequiredException(ex); // InnerException property set by constructor 
}
```

Most exception classes will take another exception as a constructor parameter allowing you to pass in the InnerException. In order to avoid losing the information, the InnerException property of exceptions is read-only, only being set in constructors.

If you're not handling the exceptions at the time that you've caught it, you generally want to just rethrow it like this:

```c#
try
{
}
catch (ArgumentNullException ex)
{
    Logger.LogError(ex); // log the error before rethrowing
    throw; 
}
```

Taking this approach will preserve the previous exception, so very little is lost by my stopping to log what happened.

## Catching Specific Exceptions
In the previous examples, you usually saw ``catch (System.Exception)``, and that's using the polymorphism you learned about from the lesson on [Objects and Classes](lesson-13.md). Knowing that, you also know that you can pass any exception that inherits from ``System.Exception``, and it can be used there as well. If you want to be more specific you can be. Our ``System.FormatException`` did exactly that.

When you catch a specific exception, it will only catch the exceptions of that specific type (or ones that inherit from it). That means that catching ``System.FormatException`` will not catch all ``System.Exception`` exceptions that are thrown. If you want to handle both, differently, you would do this:

```c#
try
{
}
catch (System.FormatException)
{
    // Handle only FormatExceptions here
}
catch (System.Exception)
{
    // Handle all other exceptions here
}
```

In some instances you may want to handle more than one type of specific exception the same way and their common exception ancestor class is only ``System.Exception``. In order to that, you use the ``when`` clause followed by a condition explaining when you want to use that catch block. It looks like this:

```c#
try
{
}
catch (Exception ex) when (ex is MemberNameNotFoundException || ex is FormatException)
{
    // Handle only MemberNameNotFoundException and FormatException here
}
```

By restricting in this way, we're able to reuse more code, while ignoring exception types we're not prepared to handle.

## When to Avoid Throwing and Catching
It is important that you only throw exceptions when it's appropriate, when your code is in an unintended situation. If the situation seems likely, but requires taking a certain action, just handle it using normal application flow control elements like ``if`` statements. 

A good rule for deciding is if the situation would not allow you to return a valid value from your method, you might need to throw an exception instead. These two examples show one case where an exception is not needed, one shows it being used and the other shows normal control flow.

```c#
public void SetMemberBirthday(int memberId, DateTime birthday)
{
    Member member = _memberList.SingleOrDefault(m => m.Id == memberId);
    if (member == null)
    {
        throw new MemberNameNotFoundException(id);
    }
    member.Birthday = birthday;
}
```

```c#
public bool SetMemberBirthday(int memberId, DateTime birthday)
{
    Member member = _memberList.SingleOrDefault(m => m.Id == memberId);
    if (member == null)
    {
        Logger.LogWarning($"SetMemberBirthday Error: Member {memberId} not found. Birthday not set {birthday}.");
        return false; // false tells the caller that the operation failed.
    }
    member.Birthday = birthday;
    return true;
}
```

**Note:** When you throw an exception, there is always the chance that the exception will go unhandled, so make sure you account for that in your coding.

## Creating Your Own Exceptions
When you are going to throw an exception in your code, it's important that you use the correct exception. This makes it easier for you, and others who call your code, to catch the specific exception that you're throwing. 

In order to create your own exceptions, you simply inherit from the ``Exception`` class or from a more-specific exception class like this:

```c#
public class MemberNameNotFoundException : Exception
{
    public MemberNameNotFoundException(string memberName) 
        : base($"Could not find member: {memberName}.")
    {}
}
```

Once you have that exception, you're able to throw it uisng the same ``throw`` command you use for any other exceptions like this:

```c#
public int GetMemberIdByName(string memberName)
{
    Member member = _memberList.SingleOrDefault(m => m.Name == memberName);
    if (member != null)
    {
        return member.Id;
    }
    throw new MemberNameNotFoundException(memberName);
}
```

You throw your exception here, because the code is in an exceptional case. You have no ``Id`` to return from your method, so you throw an exception that the caller can catch and handle. The caller of this method, might handle the exception like this:

```c#
try
{
    int memberId = membersOnlyList.GetMemberIdByName("George");
    Console.WriteLine($"Come on in member, {memberId}");
}
catch (MemberNameNotFoundException)
{
    Console.WriteLine($"You're not on the list.");
}
```

**Note:** Do not create custom exceptions inheriting from ``ApplicationException``. Instead, your custom exceptions should inherit from the base ``Exception`` class. Do not throw an ApplicationException exception in your code, and you should not catch an ApplicationException exception unless you intend to re-throw the original exception

## Next Steps

Create two custom exceptions, ``MyMissingTokenException`` and ``MyInvalidTokenException``, both inheriting from ``ArgumentException``. The first should be thrown if the token is null or empty. The second should be thrown if the token exists, but doesn't meet some criteria of your choosing. Throw them when appropriate from inside this example method:

```c#
public List<string> GetAccessPermissions(string token)
{
    // check token validity here and throw exceptions as needed.

    return new List<string>{ "ReadLessons", "ReviewLessons" };
}
```

After creating this method, call it and make sure that you're able to catch and handle the two exception types differently. Also make sure that when given a valid token, it returns the result you expect.
