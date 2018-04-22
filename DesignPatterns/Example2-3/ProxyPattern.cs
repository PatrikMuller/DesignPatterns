using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2_3
{
    class ProxyPattern
    {

        // Proxy Pattern Judith Bishop Dec 2006
        // Shows virtual and protection proxies

        class SubjectAccessor
        {

            public interface ISubject
            {
                string Request();
            }
            private class Subject
            {
                public string Request()
                {
                    return "Subject Request " + "Choose left door\n";
                }
            }

            public class Proxy : ISubject
            {
                Subject subject;

                public string Request()
                {
                    // A virtual proxy creates the object only on its first method call
                    if (subject == null)
                    {
                        Console.WriteLine("Subject inactive");
                        subject = new Subject();
                    }
                    Console.WriteLine("Subject active");
                    return "Proxy: Call to " + subject.Request();
                }
            }

            public class ProtectionProxy : ISubject
            {
                // An authentication proxy first asks for a password
                Subject subject;
                string password = "Abracadabra";

                public string Authenticate(string supplied)
                {
                    if (supplied != password)
                        return "Protection Proxy: No access";
                    else
                        subject = new Subject();
                    return "Protection Proxy: Authenticated";
                }

                public string Request()
                {
                    if (subject == null)
                        return "Protection Proxy: Authenticate first";
                    else return "Protection Proxy: Call to " +
                    subject.Request();
                }
            }
        }

        class Client : SubjectAccessor
        {
            static void Main()
            {
                Console.WriteLine("Proxy Pattern\n");

                ISubject subject = new Proxy();
                Console.WriteLine(subject.Request());
                Console.WriteLine(subject.Request());

                ProtectionProxy Subject = new ProtectionProxy();
                Console.WriteLine(Subject.Request());

                Console.WriteLine((Subject as ProtectionProxy).Authenticate("Secret"));
                Console.WriteLine((Subject as ProtectionProxy).Authenticate("Abracadabra"));
                Console.WriteLine(Subject.Request());

                Console.ReadLine();
            }
        }



    }
}
