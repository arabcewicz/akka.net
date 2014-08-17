using System;
using Akka.TestKit;
using Xunit;

namespace Akka.Testkit.Tests.TestKitBaseTests
{
    public class ExpectTests : AkkaSpec
    {
        [Fact]
        public void ExpectMsgAllOf_should_receive_correct_messages()
        {
            TestActor.Tell("1");
            TestActor.Tell("2");
            TestActor.Tell("3");
            TestActor.Tell("4");
            ExpectMsgAllOf("3", "1", "4", "2").ShouldOnlyContainInOrder("1", "2", "3", "4");
        }

        [Fact]
        public void ExpectMsgAllOf_should_fail_when_receiveing_unexpected()
        {
            TestActor.Tell("1");
            TestActor.Tell("2");
            TestActor.Tell("Totally unexpected");
            TestActor.Tell("3");
            Intercept(() => ExpectMsgAllOf("3", "1", "2"));
        }

        [Fact]
        public void ExpectMsgAllOf_should_timeout_when_not_receiving_any_messages()
        {
            Intercept(() => ExpectMsgAllOf(TimeSpan.FromMilliseconds(100), "3", "1", "2"));
        }

        [Fact]
        public void ExpectMsgAllOf_should_timeout_if_to_few_messages()
        {
            TestActor.Tell("1");
            TestActor.Tell("2");
            Intercept(() => ExpectMsgAllOf(TimeSpan.FromMilliseconds(100), "3", "1", "2"));
        }

    }
}