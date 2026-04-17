using System;
using System.Xml.Linq;
using NUnit.Framework;

namespace MythicLegion.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructorShouldWork()
        {
            Legion legion = new Legion();
            Assert.IsNotNull(legion);

            
            string expected = "No heroes in the legion.";
            var actual = legion.GetLegionInfo();

            Assert.AreEqual(expected, actual);

            Hero newHero = new Hero("name", "type");

            Assert.That(newHero.Name, Is.EqualTo("name"));
            Assert.That(newHero.Type, Is.EqualTo("type"));
            Assert.That(newHero.Health, Is.EqualTo(100));
            Assert.That(newHero.Power, Is.EqualTo(20));
            Assert.That(newHero.IsTrained, Is.False);



        }
        [Test]
        public void AddShouldWork()
        {
            Legion legion = new Legion();
            Assert.IsNotNull(legion);

            Hero newHero = new Hero("name", "type");
            
            legion.AddHero(newHero);
            
            Assert.That(legion, Is.Not.Null);
            //Assert.IsNotEmpty(legion);// testvai to string methoda

        }
        [Test]
        public void AddShouldNotWorkIfHeroIsNull()
        {
            Legion legion = new Legion();
            Assert.IsNotNull(legion);

            Hero newHero = new Hero("test", "type");


            Assert.Throws<ArgumentNullException>(() => legion.AddHero(null));


            //Assert.IsNotEmpty(legion);// testvai to string methoda

        }
        [Test]
        public void AddShouldNotWorkIfHeroAlreadyExist()
        {
            Legion legion = new Legion();
            Assert.IsNotNull(legion);

            Hero newHero = new Hero("test", "type");
            Hero newHero2 = new Hero("test", "type2");

            legion.AddHero(newHero);
            Assert.Throws<ArgumentException>(() => legion.AddHero(newHero2));


            //Assert.IsNotEmpty(legion);// testvai to string methoda

        }

        [Test]
        public void RemoveShouldReturnTrueAndRemovehero()
        {
            Legion legion = new Legion();
            Assert.IsNotNull(legion);

            Hero newHero = new Hero("test", "type");
            Hero newHero2 = new Hero("test2", "type2");

            legion.AddHero(newHero);
            legion.AddHero(newHero2);

            
            Assert.IsTrue(legion.RemoveHero("test"));
            
            string info = legion.GetLegionInfo();

            Assert.That(info, Does.Contain("test2"));

            legion.RemoveHero("test2");
            Assert.That(legion.GetLegionInfo(), Does.Not.Contain("test2"));


            string expected = "No heroes in the legion.";
            var actual = legion.GetLegionInfo();

            Assert.AreEqual(expected, actual);

            Assert.IsFalse(legion.RemoveHero("test3"));

            //Assert.IsNotEmpty(legion);// testvai to string methoda

        }

        [Test]
        public void TrainHeroShouldWork()
        {
            Legion legion = new Legion();
            Assert.IsNotNull(legion);

            Hero newHero = new Hero("test", "type");
            Hero newHero2 = new Hero("test2", "type2");

            legion.AddHero(newHero);
            legion.AddHero(newHero2);

            Assert.IsFalse(newHero.IsTrained);
            Assert.That(newHero.Power, Is.EqualTo(20));

            legion.TrainHero("test");

            string actual = legion.TrainHero("test2");
            string expected = $"test2 has been trained.";

            Assert.AreEqual(expected, actual);

            var expectedInfo = legion.GetLegionInfo();

            Assert.That(expectedInfo, Does.Contain("test (type) - Power: 30, Health: 101, Trained: True"));
         
            Assert.IsTrue(newHero.IsTrained);
            Assert.That(newHero.Power, Is.EqualTo(30));
            Assert.That(newHero.Health, Is.EqualTo(101));
            

        }

        [Test]
        public void TrainHeroShouldNotWorkWhenHeroNotFound()
        {
            Legion legion = new Legion();
            Assert.IsNotNull(legion);

            Hero newHero = new Hero("test", "type");
            Hero newHero2 = new Hero("test2", "type2");

            legion.AddHero(newHero);
            legion.AddHero(newHero2);

            string actual = legion.TrainHero("test16");
            string expected = $"Hero with name test16 not found.";

            Assert.AreEqual(expected, actual);


        }
        [Test]
        public void GetLegionInfoWorks()
        {
            Legion legion = new Legion();
            Assert.IsNotNull(legion);

            Hero newHero = new Hero("test", "type");
            Hero newHero2 = new Hero("test2", "type2");

            legion.AddHero(newHero);


            var actual = legion.GetLegionInfo();

            string expected = $"test (type) - Power: 20, Health: 100, Trained: False";

            Assert.AreEqual(expected, actual);


        }
    }
}