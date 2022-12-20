using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


namespace Tests
{
    
    public class User
    {

        public string Name { get; }
        public string Tel { get; }
        public User(string name, string tel){
            Name = name;
            Tel = tel;
           
        }


        public string GetGreeting(string template)
        {
            return StringFormatter.StringFormatter.Shared.Format(template, this);
        }

    }
}
