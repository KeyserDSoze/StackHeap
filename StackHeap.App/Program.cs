using System;

namespace StackHeap.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute();
        }
        unsafe static void Execute()
        {
            int age;  //in stack age => 0
            age = 20;  //in stack age => 20
            Person person; //in stack => 0x000000000000
            person = new Person()
            {
                Age = age,
                Name = "Ale"
            };
            //in stack person => 0x000000fa3928383 (memory in heap)
            //in heap at 0x000000fa3928383 the value new Person() { Age = age, Name = "Ale" }
            age++;
            //in stack age => 21
            //in heap Person doesn't change in his age
            Console.WriteLine("After age++: " + person.Age);
            int* pointer = &age; //in stack pointer => 0x00000caca839239 (memory in stack to another stack)
            Console.WriteLine("Memory: " + (int)pointer + " with value: " + *pointer);
            //in pointer you can find stack position of age and in *pointer the value of that stack

            Person.AgeToAdd = 1; //in heap AgeToAdd of person takes value of 1 for every instance 
            //(a static field or property is a pointer to heap)
            person = person.FetchAndAddAge();
            //first at all stack runs FetchAndAddAge (stack operation of call, execution (with his stacks (his operations)) and return)
            //then assign to person value (stack operation)
            age = person.Age;
            //age => 21 (in stack from a value in Heap)
            person.Age++;
            //person.Age => 22 (add +1 to a value in Heap)
            Console.WriteLine(age);
            //age doesn't change 
            //int* pointerOfAgeToAdd = &Person.AgeToAdd; this is a heap pointer, it's not possible to commute to a stack pointer
            Console.ReadLine();
        }
        public struct Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public static int AgeToAdd = 0;
            public Person FetchAndAddAge()
            {
                this.Age += AgeToAdd; //AgeToAdd is the same for every instance (heap pointer)
                return this;
            }
        }
    }
}
