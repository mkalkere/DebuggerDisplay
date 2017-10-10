# Enhancing Debugging with the Debugger Display Attribute


- Have you ever wondered how to increase the debugging experience in Visual Studio?

- Do you need a way to make changes to the code which impacts/works ony while debugging?

If your aneswer is ``yes`` to the above questions then,    ``DebuggerDisplay`` comes to rescue.

One of the prominent task as a developer when identifying a bug with in a code is to step through the code and analyze the ``runtime object data``.

Even though the Debugger does a good job of displaying the current object where the breakpoint is hit. It cannot determine what needs to be shown in case of the Types (objects). This becomes even more complicated when you want to find a value of a particular item in a collection containing few hundreds of items.

## Let’s take a look at the Following Example: ##
```
public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
 
        public string FullName { get => $"{FirstName} {LastName}"; }
 <bold>
        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
</bold>        
  
    }
```
We have a Simple class called Customer with two properties holding the customer _FirstName_ and _LastName_ and a read only property _FullName_ which returns the concatenated **First Name** and the **Last Name** of the customer. 

## Let’s create a console application and initialize the customer class as a collection of customers as shown below: ##

```
class Program
    {
        static void Main(string[] args)
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer("James","Butt"),
                new Customer("Josephine","Darakjy"),
                new Customer("Art","Venere"),
                new Customer("Lenna","Paprocki"),
                new Customer("Donette","Foller"),
                new Customer("Simona","Morasca")
            };
        }
    }
```
Now if we keep the breakpoint at the closing curly braces of the main method and look at the locals _(Debug->Windows->Locals or Ctrl+Alt+V,L)_. We will see Value column being displayed as **DebuggerDisplay.Customer**. This is because the Expression Evaluator is calling **_ToString()_** method on the Type.

Figure 1:

This becomes difficult when there are few hundreds of items in the list and we want inspect a particular   item in it. 

One way to display the desired value in the Value column is by _overriding the **ToString()** method_ of the **Type** to return the desired value as shown below:
```
public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
 
        public string FullName { get => $"{FirstName} {LastName}"; }
 
        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
 
        public override string ToString()
        {
            return FullName;
        }
    }
```

Even though it displays the desired value there a two drawbacks with it:

**1.    Performance**
>Since we have overridden the ToString which causes the Expression Evaluator to invoke             function which can be slower than the normal data inspection. 

**2.	 Will not be possible to have different data for debug view and the ToString View**
>If we wanted different to be shown in the debug View and different value in the Application then it is not possible.

So, what is the best way to display the desired value of the runtime object data in a simple and meaningful way?

## Debugger Display attribute comes to the rescue here. ##

### Let’s have a look at the use of DebuggerDisplay attribute in the following code. ###
```
    [DebuggerDisplay("Name: {FullName,nq}")]
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
 
        public string FullName { get => $"{FirstName} {LastName}"; }
 
        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
 ```

 The use of the _Debugger Display attribute_ for the Type directs the **Expression Evaluator** to evaluate the expression provided and display the resulting value in as a Value of the debugger variable window.

 Figure 2:

 If you see in the above picture the value column displays the ``Full Name`` in way which is easier to identify. This helps in making the process of debugging more easier especially in the cases where there is a complex type which encompasses other types and the values which we are interested in are ``nested at various levels`` which further complicates when it is a ``part of a collection``.

 **Note:** 
 > You might have noticed a Format Specifier ‘nq’ which stands for ‘no quote’ makes no quotes to be applied while displaying the debug variables.
 You can read about Format Specifiers <a href=”https://msdn.microsoft.com/en-us/library/e514eeby.aspx” target=”_blank”> here</a>

You can read further about **DebuggerDisplay attribute** over <a href=https://msdn.microsoft.com/en-us/library/e514eeby.aspx target=”_blank”> here </a>

You can find the source code of this example on my <a href=https://github.com/mkalkere/DebuggerDisplay.git target=”_blank”>Git Hub</a>

