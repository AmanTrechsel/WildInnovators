# WildInnovators
Welkom in de WildInnovators github repository. Hierin worden de volgende onderdelen behandeld:
1. [App](#App)
    - Installatie
    - Gebruikers gids
    - Developer gids
2. [Website](#Website)
    - Installatie
    - Gebruikers gids
    - Developer gids
3. [WebEditor](#WebEditor)
    - Installatie
    - Gebruikers gids
    - Developer gids
4. [Contact](#Contact)
# App
## Installatie
De app kan geïnstalleerd worden op twee manieren, namelijk via een apk gebouwd in de editor, of via de Google Play Store.

### **Benodigdheden**
- Mobiel Android apparaat met minimaal Android 9.0
- Ruimte om in te bewegen
- Goed humeur

### **Via de editor**

Om de app te installeren via de editor moet je de *Developer gids* volgen om de editor te openen.

Vervolgens moet je in ``Project / Build Settings / Player Settings / Publish Settings`` het wachtwoord voor builden geven. Dit wachtwoord heeft de projectgroep.

Daarna kan je de app als .apk bestand builden en installeren op je apparaat.

### **Via de Google Play Store**

Op je mobiele apparaat ga naar de playstore en klik op installeren:
[Play Store Link](https://play.google.com/store/apps/details?id=com.WildInnovators.TimeWise)

*QR Code:*

<img src="qr-code.png" alt= “TimeWiseAppQRCode” width=50%>

## Gebruikers gids
Wanneer je de app opent, zul je een vakkenselectie zien. Op het moment is dit slechts het vak Biologie.

Je hebt de mogelijkheid om je instellingen aan te passen op de instellingen knop rechtsbovenin. Ook is er een knop om een code in te vullen. Deze code zal je toegang geven tot meer vakken. Tenzij je een code hebt zal dit alleen interessant worden bij de gids voor de website.

Wanneer je op de Biologie-knop klikt zul je een aantal lessen zien. Om meer informatie te krijgen over een les kun je op de <img src="TimeWise\Assets\Graphics\UIGraphics\InformationIcon_outlined.png" alt= “informatie” width=2%> knop drukken. Ook kan je vanaf dit punt altijd op de terugknop rechtsonderin klikken om terug te gaan, of met de standaard terugknop van je Android apparaat. Als je helemaal terug wilt naar de vakkenselectie kan dat door op de TimeWise logo te klikken linksbovenin.

Verder zul je nog de encyclopedie-knop en zoek-knop zien. In de encyclopedie kan je meer informatie vinden over ontdekte dieren. De encyclopedie is gebaseerd op de laatste les die je hebt bekeken. Verder geeft het je progressie aan door middel van een teller.

De zoek-knop brengt je naar een apart zoek-scherm waarin je elk vak, elke les, elke instelling en elke encyclopediepagina kan zoeken op basis van de naam.

In het lessen-selectiescherm kun je een les gaan kiezen, bijvoorbeeld de oceaanbodem. Wanneer je dit kiest, zul je een popup krijgen dat je toestemming moet geven om je camera te gebruiken. Wanneer je dit hebt bevestigd, hoef je dit in de toekomst niet meer te doen. Na een paar seconden laden zul je dan de oceaanbodem AR-omgeving zien. Met wat geluk kun je het zeewier, de walvis en het plankton vinden. Wanneer je op één van deze elementen klikt zal er een vraag tevoorschijn komen. Als je dit goed beantwoordt, speel je de encyclopedie pagina daarvoor vrij. Verder kan je ook op elk moment een foto maken binnen de AR-omgeving door op de foto knop onderaan in het midden te klikken.

Verder worden alle data die gebruikt worden binnen de app, zoals toestemming, encyclopedie voortgang, foto's en lesdata, allemaal lokaal op je apparaat opgeslagen. Dus er wordt niks gedaan met je data en je kan het zo weer verwijderen. Buiten je eigen apparaat heeft dus niemand toegang tot je data.

## Developer gids
### **Benodigdheden**
- Een Windows/MacOS/Linux apparaat dat Unity3D kan uitvoeren.
- Unity 2021.3.23f1
- Geduld

### **Installatie**
1. Download/clone de nieuwste versie van deze github en zet dit op een locatie die makkelijk terug te vinden is (niet gecomprimeerd).

2. Download en installeer [Unity Hub](https://unity.com/download) als je dit nog niet hebt gedaan.

3. Activeer een license binnen Unity Hub als dit nog moet.

4. Installeer Unity 2021.3.23f1 onder ``Installs`` in Unity Hub als je dit nog niet hebt gedaan. De installatie van Unity kan even duren.

5. Als er een popup tevoorschijn komt om een build versie te installeren, selecteer dan ``Android``.

6. Onder ``Projects`` klik op het pijltje naast ``Open`` rechtsbovenin en klik dan op ``Add project from disk``. Selecteer dan de ``TimeWise`` map binnen de folder die je van github hebt geïnstalleerd.

7. Open het project, als er ``Version Mismatch`` staat, moet je de goede versie (Unity 2021.3.23f1) selecteren. Het openen van het project kan even duren de eerste keer.

8. Als je nu een Unity project voor je hebt met mappen als ``AddressableAssetsData`` en ``Fonts`` onderaan heb je de installatie voltooid.

*Alleen als je stap 5 hebt overgeslagen.*

9.  Klik linksbovenin op ``Project / Build Settings`` en selecteer je het Android icoontje. Unity zal dan vragen om de Android Build Platform te installeren. Volg de installatie instructies.

10. Wanneer de installatie klaar is kun je in de build settings op ``Switch platform`` klikken en dan zal het project opnieuw compilen. Soms helpt het hierna om het project opnieuw te openen.

### **Content toevoegen**
Nieuwe content toevoegen binnen de app is erg makkelijk. Natuurlijk kan je nieuwe lessen aanmaken op de website, maar hier heb je alleen toegang voor als je de code hebt. Als je een les wilt creëren moet je beginnen met een `Prefab` maken van het model dat je wilt gebruiken. Dit is een opgeslagen spelobject met jouw data als het 3D-object, animaties en materialen erop. [Hier is een oude, maar handige tutorial door Jimmy Vegas hoe je dit doet](https://www.youtube.com/watch?v=wS5f0OKucxM).

Hierna moet je bovenaan, op ``Custom/ARItem Editor`` klikken. Dit is een eigen gemaakt editor-scherm waarin je je prefab kan omtoveren naar een ARItem. Klik op de (+) symbool om een ARItem aan te maken en geef het een naam. Daarna kun je velden aanpassen zoals een standaard positie / rotatie offset te geven. Maar het belangrijkste is om de prefab erin te stoppen. Doe dit voor al je prefabs die je wilt toevoegen.

Daarna kun je naar ``Custom/Subject Editor`` om een les aan te maken. Dit gaat op dezelfde manier als de ARItem Editor. Echter, je moet wel op ``Highlight Selected Subject`` klikken en de waardes aanpassen in de Inspector aan de rechterkant. Dit komt door een bug met deze versie van Unity en kan dus ook niet opgelost worden zonder de versie up te daten.

Deze zelfde stappen gelden voor het maken van een nieuwe encyclopediepagina, instellingen-knop en voor een nieuw vak. Hoewel als je een nieuwe instelling aanmaakt moet je de functionaliteiten er wel van toevoegen aan de ``SettingManager.cs`` script en als je de instellingen ook wilt opslaan regel je dat onder ``AppManager.cs`` en ``SettingsData.cs``. Hiervoor kun je de bestaande instellingen gebruiken als basis en deze kopiëren en aanpassen.

Verder wordt het meeste binnen de App behandeld door de ``*Manager.cs`` scripts. Al deze scripts hebben hun eigen taken. De belangrijkste is dan ook de ``AppManager.cs``. Deze is te vinden in de ``CourseSelect`` scene en regelt de meeste globale app zaken.

# Website
## Installatie
De website is volledig toegankelijk op https://timewise.serverict.nl/. Hiervoor moet je wel een werkende internet verbinding hebben (niet Eduroam) en een geschikte browser gebruiken. (Firefox of een form van Chromium zou moeten werken.).
## Gebruikers gids
Binnen in de website spreken de navigatie en het doel van de website voor zich. Als we naar de Inlog-pagina gaan, kun je inloggen of registreren. Dit is functioneel. Echter, de wachtwoord vergeten-knop doet niks gezien accounts ook niet gelinkt zijn aan een emailadres.

Wanneer je ingelogd bent, zal de inlog-knop ook veranderen naar een dashboard-knop. Hier kun je een groep of les aanmaken. Voor het maken van een les moet je een aantal velden invullen waaronder modellen. Hiervoor wordt de [WebEditor](#WebEditor) gebruikt.
## Developer gids
Het aanpassen en toevoegen van nieuwe elementen aan de website is zo makkelijk als de HTML/css en PHP aan te passen of eraan toe te voegen. Hierbij is het wel belangrijk om de sessies te behouden door middel van een `session_start();` methode.

Ook wordt er gebruik gemaakt van een ``constants.php`` om de website te linken aan een SQL Database. Hiervoor word PDO gebruikt. De inloggegevens worden gegeven door de projectgroep.

### **Voor eigen verder gebruik**
### **Benodigdheden**
- Een form van webhosting (Docker, FTP of Server hosting)
- SQL Database
- Internet connectie
- Paracetamol (tegen de hoofdpijn)
### **Installatie**
1. Download de website-bestanden van deze github pagina (de /Website folder).

2. Upload de website-bestanden naar je form van webhosting. Deze stap zal variëren op basis van je webhosting.

3. Zet je SQL Database klaar door middel van de gegeven infrastructuur.

4. Vul de gegevens in je ``constants.php`` met je inloggegevens in je SQL Database.

5. Je website is klaar voor gebruik!

# WebEditor
## Installatie
De WebEditor werkt op zichzelf vanuit de website. Log in en maak een les om de WebEditor te gebruiken.
## Gebruikers gids
Navigeer naar https://timewise.serverict.nl/lesson.php om de WebEditor te gebruiken. *Je moet wel ingelogd zijn om dit te gebruiken!*

Om de WebEditor te gebruiken moet je een .obj bestand hebben. Je kan dit model dan uploaden met de upload knop. Soms duurt het even voor dit model geladen is. Vanaf hier kun je attributen aanpassen naar hoe je het wilt.

Wanneer je model klaar is kun je op Afronden drukken!
## Developer gids
### **Benodigdheden**
- Een Windows/MacOS/Linux apparaat dat Unity3D kan uitvoeren.
- Unity 2021.3.23f1
- Een OBJ bestand om mee te testen
### **Installatie**
De installatiestappen voor de WebEditor zijn vergelijkbaar met die van de app. Hier worden ze opnieuw herhaald met aanpassingen voor de WebEditor.

1. Download/clone de nieuwste versie van deze github en zet dit op een locatie die makkelijk terug te vinden is (niet gecomprimeerd).

2. Download en installeer [Unity Hub](https://unity.com/download) als je dit nog niet hebt gedaan.

3. Activeer een license binnen Unity Hub als dit nog moet.

4. Installeer Unity 2021.3.23f1 onder ``Installs`` in Unity Hub als je dit nog niet hebt gedaan. De installatie van Unity kan even duren.

5. Als er een pop up tevoorschijn komt om een build versie te installeren, selecteer dan ``WebGL``.

6. Onder ``Projects`` klik op het pijltje naast ``Open`` rechtsbovenin en klik dan op ``Add project from disk``. Selecteer dan de ``TimeWise`` map binnen de folder die je van github hebt geïnstalleerd.

7. Open het project, als er ``Version Mismatch`` staat moet je de goede versie (Unity 2021.3.23f1) selecteren. Het openen van het project kan even duren de eerste keer.

8. Als je nu een Unity project voor je hebt met mappen als ``AddressableAssetsData`` en ``Fonts`` onderaan heb je de installtie voltooid.

*Alleen als je stap 5 hebt overgeslagen.*

9.  Klik linksbovenin op ``Project / Build Settings`` en selecteer je het WebGL icoontje. Unity zal dan vragen om de WebGL Build Platform te installeren. Volg de installatie instructies.

10. Wanneer de installatie klaar, is kun je in de build settings op ``Switch platform`` klikken en dan zal het project opnieuw compilen. Soms helpt het hierna om het project opnieuw te openen.

# Contact
Voor meer informatie kun je contact opnemen met de projectgroep bij:
kimmy.visscher@student.nhlstenden.com