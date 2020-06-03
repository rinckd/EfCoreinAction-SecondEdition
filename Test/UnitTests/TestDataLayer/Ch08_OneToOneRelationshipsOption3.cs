﻿// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Linq;
using Test.Chapter08Listings.EfClasses;
using Test.Chapter08Listings.EFCode;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests.TestDataLayer
{
    public class Ch08_OneToOneRelationshipsOption3
    {
        [Fact]
        public void TestOption3OneToOneAddOk()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<Chapter08DbContext>();
            using (var context = new Chapter08DbContext(options))
            {
                context.Database.EnsureCreated();

                //ATTEMPT
                var ticket = new TicketOption3
                {
                    Attendee = new Attendee{Name = "Person1", TicketOption1 = new TicketOption1(), Required = new RequiredTrack()}
                };
                context.Add(ticket);
                context.SaveChanges();

                //VERIFY
                context.Attendees.Count().ShouldEqual(1);
                context.TicketOption3s.Count().ShouldEqual(1);
                ticket.Attendee.ShouldNotBeNull();
            }
        }

        [Fact]
        public void TestOption3OneToOneDeleteOk()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<Chapter08DbContext>();
            using (var context = new Chapter08DbContext(options))
            {
                context.Database.EnsureCreated();
                var ticket = new TicketOption3
                {
                    Attendee = new Attendee { Name = "Person1", TicketOption1 = new TicketOption1(), Required = new RequiredTrack() }
                };
                context.Add(ticket);
                context.SaveChanges();

                //ATTEMPT
                context.Remove(ticket);
                context.SaveChanges();

                //VERIFY
                context.Attendees.Count().ShouldEqual(1);
                context.TicketOption3s.Count().ShouldEqual(0);
            }
        }

        [Fact]
        public void TestOption3OneToOneNoTicketOk()
        {
            //SETUP
            var options = SqliteInMemory.CreateOptions<Chapter08DbContext>();
            using (var context = new Chapter08DbContext(options))
            {
                context.Database.EnsureCreated();

                //ATTEMPT
                var attendee = new Attendee
                {
                    Name = "Person1",
                    TicketOption1 = new TicketOption1(),
                    Required = new RequiredTrack()
                };
                context.Add(attendee);
                context.SaveChanges();

                //VERIFY
                context.Attendees.Count().ShouldEqual(1);
                context.TicketOption3s.Count().ShouldEqual(0);
                attendee.TicketOption3.ShouldBeNull();
            }
        }


    }
}