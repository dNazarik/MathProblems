This is a bit out of KISS for a kind of simple game 
Just showing that I understand some things :)
I tried to write the code so I can scale it in the future :)

This solution contains:
- single entry point;
- DI(VContainer);
- (pseudo)MVC;
- structured hierarchy (I hope)
- a few Unit tests
- BG change via Addressables
- Scoreboard (JSON)
- Assemblies
- Dynamic UI (at least UI items don't overlap each other :) )

Notes:

I have added a couple of Unit tests which was outside of the test task;
I have added audios that also was outside of the test task, you can enable/disable it via the game settings config;
I didn't use StringBuilder because there is not much text. In most projects, strings don't cause performance/memory issues;
Without QA it is hard to catch everything, so if you find something -- let me know, I will fix it :)

Also, if you want me to add/remove/change something -- I will do it with a pleasure :)
