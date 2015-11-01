### Telesharp 
Telesharp is libary, which can help you to create bots for Telegram with C#.

#### Creating bots
<b>Use the token sent by [@BotFather](http://telegram.me/BotFather)</b>


You can create bot, who doing nothing:

```csharp
var Bot = new Bot(INSERT_YOUR_TOKEN_HERE);
Bot.Run();
Bot.WaitToDie();
```

You can create bot, who send random zombie sounds:
```csharp
var zombieSounds = new []{"aggrh", "zzz", "grrrr", "arrr", "ogrrrh"};
var Bot = new Bot(INSERT_YOUR_TOKEN_HERE);
Bot.OnParseMessage += (o,e) =>
{
    Bot.Methods.SendMessage(e.Message.Chat, 
        zombieSounds[(new Random(DateTime.Now.Millisecond))
            .Next(0, zombieSounds.Length)]);    
};
Bot.Run();
Bot.WaitToDie();
```

#### Also you can view examples:

1. [Message listener](https://github.com/DaFri-Nochiterov/Telesharp/wiki/Create-message-listener-bot-and-upgrade-it-to-sticker-sender-bot#create-message-listener-bot-and-upgrade-it-to-sticker-sender-bot>)
2. [Sticker-Sender bot](https://github.com/DaFri-Nochiterov/Telesharp/wiki/Create-message-listener-bot-and-upgrade-it-to-sticker-sender-bot#upgrade-bot)
3. [LIKE5 wallpapers bot](https://github.com/DaFri-Nochiterov/LIKE5Bot_Telegram)

#### API

Not all methods included in this API.

Excluded:

• [setWebhook([Optional] String url, [Optional] InputFile certificate)](https://core.telegram.org/bots/api#setwebhook)

It is needed? Vote [here](https://github.com/DaFri-Nochiterov/Telesharp/issues/1)

<b>Temporarily</b> not included:
- [ ] sendCustomRequest(String uri, Dictionary&lt;String, Object&gt; parametrs)
- [x] [sendVoice(Integer chat_id, InputFile voice, [Optional] duration, [Optional] reply_to_message_id, [Optional] reply_markup)](https://core.telegram.org/bots/api#sendvoice)

You can get help on using the API bots [here](https://core.telegram.org/bots/api)

[Bots FAQ](https://core.telegram.org/bots/faq)