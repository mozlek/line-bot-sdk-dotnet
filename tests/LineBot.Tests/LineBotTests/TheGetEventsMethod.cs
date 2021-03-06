﻿// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        private const string WebhookJson = "Events/Webhook.json";

        [TestClass]
        public class TheGetEventsMethod
        {
            [TestMethod]
            public async Task ShouldLogRequest()
            {
                var logger = new TestLogger();
                var bot = TestConfiguration.CreateBot(logger);
                var request = new TestHttpRequest(WebhookJson);

                await bot.GetEvents(request);

                var actual = Encoding.UTF8.GetString(logger.LogReceivedEventsEventsData);

                var bytes = await File.ReadAllBytesAsync(WebhookJson);
                var expected = Encoding.UTF8.GetString(bytes).Substring(1); // Skip preamable.

                Assert.AreEqual(expected, actual);
            }
        }
    }
}
