<?php session_start()?> <!-- Session started on every page, in case someone is logged in -->
<?php ini_set('display_errors', 0); ?> <!-- You set the display errors on zero -->
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="./style/style.css">
    <title>Wild Innovators</title>
</head>
<body>
    <div id="mainContainer">
    <?php if ($_SESSION['username'] == NULL){?> <!-- Check if the username is used if so than the navbar changed from login to dashboard -->
        <header>
            <img src="./images/LogoGroepjeWhite.png">
            <nav>
                <ul>
                    <li><a href="index.php">Home</a></li>
                    <li><a id="NavText" href="handleiding.php">Handleiding</a></li>
                    <li><a href="inlog.php">Login</a></li>
                </ul>
            </nav>
        </header>
        
        <?php } 
        else{
        ?>
        <header id="dashboardHeader">
            <img src="./images/LogoGroepjeWhite.png">
            <nav>
                <ul>
                    <li><a href="index.php">Home</a></li>
                    <li><a id="NavText" href="handleiding.php">Handleiding</a></li>
                    <li><a href="dashboard.php">Dashboard</a></li>
                </ul>
            </nav>
        </header>
        <?php }
        ?>
        <div id="downloaden">
            <h1>Waar te downloaden?</h1>
            <p>TimeWise is te downloaden in de Google playstore, de app is alleen nog te gebruiken voor Android gebruikers.</p>
        </div>
        <div id="googleLogo">
            <img src="./images/googleLogo.png" alt="googleLogo">
        </div>
        <div id="quote">
            <img src="./images/balk.png" alt="quote">
        </div>
        <main>
            <h1>Hoe te gebruiken?</h1>
            <p>Wanneer je de app opent krijg je een selectiemenu te zien, waarbij je keuze hebt in verschillende vakken.
                 Wanneer je op een desbetreffende vak klikt kom je in een andere selectiemenu. 
                 Hierbij ook weer de keuze voor een bepaalde onderwerp wat aansluit aan de leerstof die op dat moment plaats vind.</p>
        </main>
        <div id="frontPageApp">
            <img src="./images/frontPage.png" alt="frontPage">
        </div>
        <footer>
            <img src="./images/LogoGroepjeWhite.png">
            <nav>
                <ul>
                    <li>Copyright</li>
                    <li>Contact</li>
                    <li><a href="policy.php">Policy</a></li>
                </ul>
            </nav>
        </footer>
    </div>
</body>
</html>