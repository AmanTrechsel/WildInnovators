<?php session_start(); ?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="./style/style.css">
    <script src="scripts/popup.js"></script>
    <title>Wild Innovators</title>
</head>
<body>
    <div id="mainContainer">
        <?php if ($_SESSION['username'] == NULL){?>
        <header>
            <img src="./images/LogoGroepjeWhite.png">
            <nav>
                <ul>
                    <li><a id="NavText" href="#">Home</a></li>
                    <li><a href="handleiding.html">Handleiding</a></li>
                    <li><a href="inlog.php">Login</a></li>
                </ul>
            </nav>
        </header>
        
        <?php } 
        else{
        ?>
                <header>
            <img src="./images/LogoGroepjeWhite.png">
            <nav>
                <ul>
                    <li><a id="NavText" href="#">Home</a></li>
                    <li><a href="handleiding.html">Handleiding</a></li>
                    <li><a href="dashboard.php">Dashboard</a></li>
                </ul>
            </nav>
        </header>
        <?php }
        ?>
        <main>
            <h1>TimeWise: de kennis op je device</h1>
            <p>TimeWise een interactieve app voor in het onderwijs.</p>
        </main>
        <div class="popup">
            <button onclick = "popupClose()" class="closeButton">&times;</button>
            <h2>Contact gegevens</h2>
            <p>Telefoonnummer: 06123456789</p>
            <p>Adres: PostbodeLaan 168 Emmen</p>
            <p>Postcode: 8901 NH Emmen</p>
        </div>
        <div id="timeWiseLogo">
            <img src="./images/AppLogoPNG.png" alt="TimeWise">  
        </div>
        <div id="buttonHomePage">
            <form  id="login" method="GET" action="inlog.php">
                <input class="buttonDashboard" type="submit" name="register" value="Naar Login Pagina">                    
            </form>
            <button onclick = "popupOpen()" id="openPopup">Contact</button>
        </div>
        <div id="aboutTimeWise">
            <div id="aboutTimeWisetext">
                <h1>Voor het onderwijs</h1>
                <p>TimeWise is een interactieve app voor in het onderwijs. Waarbij er gebruik wordt gemaakt van augumented reality.
                    Waardoor wij verschillende objecten in de echte wereld kunnen plaatsen, zodat leren effectiever, leuker en een leerzame proces word.</p>
            </div>
            <img src="./images/App1.png" alt="Voorbeeld App">
        </div>
        <div id="aboutUs">
            <div id="aboutUstext">
                <h1>Wij zijn Wild Innovators</h1>
                <p>Hallo, wij zijn eerste jaars ICT studenten aan het NHL-Stenden in Emmen. Ons is gevraagd om een innovatief product te realiseren.</p>
                <p>Ons product ‘TimeWise’ is een augumented reality app voor in het onderwijs.</p>
            </div>
            <img id="arrow" src="./images/Arrow.png" alt="Pijl">
            <img src="./images/GroupPhoto.png" alt="Groeps Foto">
        </div>
        <footer>
            <img src="./images/LogoGroepjeWhite.png">
            <nav>
                <ul>
                    <li>Copyright</li>
                    <li><button id="openPopup">Contact</button></li>
                    <li><a href="policy.html">Policy</a></li>
                </ul>
            </nav>
        </footer>
    </div>
</body>
</html>
