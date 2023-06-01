(function() {
    var quotes, totalQuotes;

    quotes = ["Het leven is 10% wat er met je gebeurt en 90% hoe je erop reageert.",
    "De toekomst behoort toe aan degenen die geloven in de schoonheid van hun dromen.",
    "Succes is niet de sleutel tot geluk. Geluk is de sleutel tot succes. Als je houdt van wat je doet, zul je succesvol zijn.",
    "Het leven is kort, en het is jouw verantwoordelijkheid om het betekenisvol te maken.",
    "Wees de verandering die je in de wereld wilt zien.",
    "Geloof in jezelf en alles is mogelijk.",
    "Elke dag is een nieuwe kans om te groeien.",
    "Volg je hart, maar neem je hersenen mee.",
    "Grootse dingen komen voort uit nederige beginselen.",
    "Durf te dromen, durf te geloven, durf te volharden.",
    "Het leven wordt niet gemeten door het aantal ademhalingen dat we nemen, maar door de momenten die onze adem benemen.",
    "De beste manier om iets gedaan te krijgen, is door het te beginnen.",
    "Geloof in jezelf en alles wat je bent. Weet dat er iets binnenin jou is dat groter is dan elke uitdaging.",
    "Wees niet bang om risico's te nemen, want alleen zo kun je echt groeien.",
    "Het leven is een avontuur, durf het te omarmen.",
    "Succes komt voort uit het niet bang zijn om te falen.",
    "Je bent sterker dan je denkt en slimmer dan je denkt.",
    "Je hebt de kracht om je eigen verhaal te schrijven.",
    "De enige persoon die je kan tegenhouden, ben je zelf.",
    "Grote dromen beginnen met kleine stappen.",
    "Wees niet bang om groot te denken.",
    "Vind vreugde in de reis, niet alleen in de bestemming.",
    "Het is nooit te laat om te worden wie je wilt zijn.",
    "Elke dag is een kans om te leren en te groeien.",
    "Het leven is kort, maak er iets buitengewoons van.",
    "Je bent niet hier om een gemiddeld leven te leiden. Je bent hier om een buitengewoon leven te leiden.",
    "Laat je angsten je niet tegenhouden om je dromen na te jagen.",
    "Je kunt meer bereiken dan je denkt, geloof in jezelf.",
    "Je kunt de wereld veranderen, begin bij jezelf.",
    "Iedere dag is een nieuwe kans om te stralen.",
    "Wacht niet op het perfecte moment, maak het moment perfect.",
    "Succes komt voort uit doorzettingsvermogen.",
    "Je bent niet machteloos, je hebt de kracht om je leven te veranderen.",
    "Wees moedig en neem actie, zelfs als je bang bent.",
    "Er is geen limiet aan wat je kunt bereiken.",
    "Streef naar vooruitgang, niet naar perfectie.",
    "Durf anders te zijn, dat is waar de magie begint.",
    "Je hebt alles in huis om succesvol te zijn.",
    "Wees de beste versie van jezelf.",
    "Elke dag is een kans om te groeien en te evolueren.",
    "Geluk komt voort uit het doen van de dingen die je passie aanwakkeren.",
    "Je bent hier voor een reden, ontdek je doel en leef het.",
    "Sta open voor nieuwe mogelijkheden en kansen.",
    "Wees dankbaar voor wat je hebt en werk voor wat je wilt.",
    "Je hebt de macht om je gedachten te veranderen en je leven te transformeren.",
    "Het leven is een reis, geniet van elke stap.",
    "Durf groot te dromen, want alleen zo kun je iets groots bereiken.",
    "Focus op wat je kunt veranderen, niet op wat buiten je controle ligt.",
    "Laat je niet beperken door je angsten, laat je leiden door je dromen.",
    "Je bent in staat tot geweldige dingen, geloof in jezelf.",
    "Richt je op de mogelijkheden, niet op de beperkingen.",
    "Wees niet bang om te falen, wees bang om nooit te proberen.",
    "Elke dag is een nieuwe kans om te stralen.",
    "De grootste beperkingen in het leven zijn degene die we onszelf opleggen.",
    "Omring jezelf met mensen die je inspireren en motiveren.",
    "Wees geduldig, goede dingen hebben tijd nodig om te groeien.",
    "Je bent niet geboren om te volgen, maar om te leiden.",
    "De wereld heeft behoefte aan jouw unieke bijdrage, wees niet bang om jezelf te laten zien.",
    "Sta open voor verandering, dat is waar groei begint.",
    "Je bent krachtiger dan je denkt, geloof in jezelf.",
    "Volg je passie en laat je hart je leiden.",
    "Je hebt de kracht om je eigen geluk te creëren.",
    "Wees vriendelijk voor jezelf en anderen.",
    "Het leven is te kort om je tijd te verspillen aan negativiteit.",
    "De toekomst is helderder dan je denkt.",
    "Geloof in jezelf, je kunt meer dan je denkt.",
    "Je hebt alles in huis om succesvol te zijn, geloof erin.",
    "Laat je angsten je niet tegenhouden om je potentieel te bereiken.",
    "Blijf jezelf uitdagen en groeien, er is geen limiet aan wat je kunt bereiken.",
    "Het leven is een avontuur, durf het te omarmen.",
    "Wees moedig, wees authentiek, wees jezelf.",
    "Je kunt meer bereiken dan je denkt, geloof in jezelf.",
    "Elke dag is een nieuwe kans om te leren en te groeien.",
    "De wereld heeft behoefte aan jouw unieke talenten en gaven.",
    "Richt je op wat je kunt beïnvloeden, laat de rest los.",
    "Je bent sterker dan je denkt, slimmer dan je denkt en dapperder dan je denkt.",
    "Wees dankbaar voor de kleine dingen in het leven.",
    "Streef naar vooruitgang, niet naar perfectie.",
    "Je hebt de macht om je eigen geluk te creëren.",
    "Het leven is te kort om je te laten tegenhouden door angst.",
    "Geloof in jezelf, want als je dat niet doet, wie zal het dan doen?",
    "Wees niet bang om anders te zijn, dat is wat je zo bijzonder maakt.",
    "Blijf niet stilstaan in je comfortzone, dat is waar groei begint.",
    "Het leven is wat je ervan maakt, dus maak er iets geweldigs van.",
    "Je hebt de kracht om je eigen verhaal te schrijven.",
    "Wees niet bang om risico's te nemen, dat is waar de beloningen liggen.",
    "Elke dag is een kans om te beginnen met het leven dat je wilt.",
    "Geloof in de kracht van mogelijkheden.",
    "Het leven is te kort om jezelf te beperken, durf groots te dromen.",
    "Je bent hier om een verschil te maken, laat je licht schijnen.",
    "Wees de verandering die je in de wereld wilt zien.",
    "Je hebt de vrijheid om je eigen pad te kiezen, maak er gebruik van.",
    "Geloof in jezelf en alles is mogelijk.",
    "Het leven wordt niet gemeten door het aantal ademhalingen dat we nemen, maar door de momenten die onze adem benemen.",
    "Durf te dromen, durf te geloven, durf te volharden.",
    "Het leven is te kort om te wachten op perfectie, begin nu.",
    "Je hebt het in je om te slagen, vertrouw op jezelf.",
    "De weg naar succes is geplaveid met obstakels, maar geef niet op.",
    "Blijf gefocust op je doelen, zelfs als het moeilijk wordt.",
    "Jij bent de regisseur van je eigen leven, maak er een meesterwerk van."];

    totalQuotes = quotes.length;

    function getQuote() {
      var activeQuotes;
      activeQuotes = quotes[Math.floor(Math.random() * totalQuotes)];
      document.getElementById('quoteContainer').textContent = activeQuotes;
    }

    getQuote();
  })();