using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using scorecard.Models;

namespace scorecard.Data
{
    public class RAGInitializer : System.Data.Entity.DropCreateDatabaseAlways<RAGContext>
    {
        protected override void Seed(RAGContext context)
        {
            var groups = new List<Group>
            {
                new Group{Title="Group 1"},
                new Group{Title="Group 2"},
                new Group{Title="Group 3"}
            };

            groups.ForEach(g => context.Groups.Add(g));
            context.SaveChanges();

            var criteria = new List<Criteria>
            {
                new Criteria{Title="Criteria 1", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Red},
                new Criteria{Title="Criteria 2", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Amber},
                new Criteria{Title="Criteria 3", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Green},
                new Criteria{Title="Criteria 4", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Red},
                new Criteria{Title="Criteria 5", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Amber},
                new Criteria{Title="Criteria 6", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Green},
                new Criteria{Title="Criteria 7", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Red},
                new Criteria{Title="Criteria 8", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Amber},
                new Criteria{Title="Criteria 9", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Green}
            };

            criteria.ForEach(c => context.Criteria.Add(c));
            context.SaveChanges();

            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Password@123");
                    
            var users = new List<ApplicationUser>
            {
                new ApplicationUser{UserName="john@smith.com",PasswordHash=password,PhoneNumber="777888"}
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var statuses = new List<StatusUpdate>
            {
                new StatusUpdate{Text="Test Update 1",StateFrom=CriteriaState.Red,StateTo=CriteriaState.Red,Stamp=DateTime.Now,Criteria=criteria[0],User=users[0]}
            };

            statuses.ForEach(s => context.StatusUpdates.Add(s));
            context.SaveChanges();
        }
    }
}