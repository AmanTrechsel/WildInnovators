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
        <main>
            <div id="createGroup">
                <h1>Group creëren</h1>
                <form method="POST" action="dashboard.php">
                    <h4>Groepsnaam</h4>
                    <p><input type="text" name="groupsName" placeholder="Mijn groepsnaam..."></p>
                    <h4>Onderdelen</h4>
                    <p><input type="checkbox" id="lesson1" name="lesson1" value="lesson1">
                    <label for="lesson1">Placeholder</label></p>
                    <p><input type="checkbox" id="lesson2" name="lesson2" value="lesson2">
                    <label for="lesson2">Placeholder</label></p>
                    <p><input type="checkbox" id="lesson3" name="lesson3" value="lesson3">
                    <label for="lesson3">Placholder</label></p>
                    <input type="submit" name="createGroup" value="Creëer">
                </form>
            </div>
        </main>
        <footer>
            <img src="./images/LogoGroepjeWhite.png">
            <nav>
                <ul>
                    <li>Copyright</li>
                    <li>Contact</li>
                    <li><a href="policy.html">Policy</a></li>
                </ul>
            </nav>
        </footer>
    </div>
</body>
</html>
