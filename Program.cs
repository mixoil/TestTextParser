using TestKSK;
using TestTextParser.Services;

await new AppDbContextInitializer().Migrate();

var poller = new InputPoller();
await poller.StartPolling();
