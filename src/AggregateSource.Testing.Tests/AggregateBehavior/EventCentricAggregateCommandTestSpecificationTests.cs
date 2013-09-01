﻿using System;
using NUnit.Framework;

namespace AggregateSource.Testing.AggregateBehavior
{
    [TestFixture]
    public class EventCentricAggregateCommandTestSpecificationTests
    {
        EventCentricAggregateCommandTestSpecification _sut;

        [SetUp]
        public void SetUp()
        {
            Func<IAggregateRootEntity> sutFactory = () => (IAggregateRootEntity) null;
            var givens = new[] {new object(), new object()};
            Action<IAggregateRootEntity> when = _ => { };
            var thens = new[] {new object(), new object()};

            _sut = new EventCentricAggregateCommandTestSpecification(
                sutFactory,
                givens,
                when,
                thens);
        }

        [Test]
        public void SutFactoryCanNotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new EventCentricAggregateCommandTestSpecification(
                null, 
                new object[0], 
                _ => { }, 
                new object[0]));
        }

        [Test]
        public void GivensCanNotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new EventCentricAggregateCommandTestSpecification(
                () => null, 
                null,
                _ => { },
                new object[0]));
        }

        [Test]
        public void WhenCanNotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new EventCentricAggregateCommandTestSpecification(
                () => null, 
                new object[0],
                null,
                new object[0]));
        }

        [Test]
        public void ThenCanNotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new EventCentricAggregateCommandTestSpecification(
                () => null, 
                new object[0],
                _ => { },
                null));
        }

        [Test]
        public void UsingDefaultCtorReturnsInstanceWithExpectedProperties()
        {
            Func<IAggregateRootEntity> sutFactory = () => (IAggregateRootEntity)null;
            var givens = new[] { new object(), new object() };
            Action<IAggregateRootEntity> when = _ => { };
            var thens= new[] { new object(), new object() };

            var sut = new EventCentricAggregateCommandTestSpecification(
                sutFactory,
                givens,
                when,
                thens);

            Assert.That(sut.SutFactory, Is.SameAs(sutFactory));
            Assert.That(sut.Givens, Is.EquivalentTo(givens));
            Assert.That(sut.When, Is.SameAs(when));
            Assert.That(sut.Thens, Is.EquivalentTo(thens));
        }

        [Test]
        public void PassReturnsExpectedResult()
        {
            var result = _sut.Pass();

            Assert.That(result.Specification, Is.SameAs(_sut));
            Assert.That(result.Passed, Is.True);
            Assert.That(result.Failed, Is.False);
            Assert.That(result.ButEvents, Is.EqualTo(Optional<object[]>.Empty));
            Assert.That(result.ButException, Is.EqualTo(Optional<Exception>.Empty));
        }

        [Test]
        public void FailReturnsExpectedResult()
        {
            var result = _sut.Fail();

            Assert.That(result.Specification, Is.SameAs(_sut));
            Assert.That(result.Passed, Is.False);
            Assert.That(result.Failed, Is.True);
            Assert.That(result.ButEvents, Is.EqualTo(Optional<object[]>.Empty));
            Assert.That(result.ButException, Is.EqualTo(Optional<Exception>.Empty));
        }

        [Test]
        public void FailEventsCanNotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.Fail((object[])null));
        }

        [Test]
        public void FailEventsReturnsExpectedResult()
        {
            var actual = new[] { new object(), new object() };

            var result = _sut.Fail(actual);

            Assert.That(result.Specification, Is.SameAs(_sut));
            Assert.That(result.Passed, Is.False);
            Assert.That(result.Failed, Is.True);
            Assert.That(result.ButEvents, Is.EqualTo(new Optional<object[]>(actual)));
            Assert.That(result.ButException, Is.EqualTo(Optional<Exception>.Empty));
        }

        [Test]
        public void FailExceptionCanNotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.Fail((Exception)null));
        }

        [Test]
        public void FailExceptionReturnsExpectedResult()
        {
            var actual = new Exception();

            var result = _sut.Fail(actual);

            Assert.That(result.Specification, Is.SameAs(_sut));
            Assert.That(result.Passed, Is.False);
            Assert.That(result.Failed, Is.True);
            Assert.That(result.ButEvents, Is.EqualTo(Optional<object[]>.Empty));
            Assert.That(result.ButException, Is.EqualTo(new Optional<Exception>(actual)));
        }

    }
}
