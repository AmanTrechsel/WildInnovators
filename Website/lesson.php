<?php
    session_start();
    require_once 'constants.php';

    $username = $_SESSION['username'];

    $errors = [];

    if ($_SERVER['REQUEST_METHOD'] == "POST")
    {
        $groupName = filter_input(INPUT_POST, "groupName");
        $lessonName = filter_input(INPUT_POST, "lessonName");
        $subject = filter_input(INPUT_POST, "subject");
        try{
            $dbHandler = new PDO ("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", "{$dbuser}", "{$dbpassword}");

        
            try{
                    $stmt = $dbHandler->prepare("INSERT INTO  `lessons` (`name`, `subject`, `groupId`) VALUES (:lessonName, :subjects, :groupID);");
                    $stmt->bindParam("lessonName", $lessonName, PDO::PARAM_STR);
                    $stmt->bindParam("subjects", $subject, PDO::PARAM_STR);
                    $stmt->bindParam("groupID", $groupID, PDO::PARAM_STR);
                    $stmt->execute();
                }
                catch (Exception $ex)
                {
                    $errors[] = $ex->getMessage();
                }
        }
        catch (Exception $ex)
        {
            $errors[] = $ex->getMessage();
        }
    }
?>  

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
        <header id="dashboardHeader">
            <img src="./images/LogoGroepjeWhite.png">
            <nav>
                <ul>
                    <li><a href="index.php">Home</a></li>
                    <li><a href="handleiding.php">Handleiding</a></li>
                    <li><a href="dashboard.php">Dashboard</a></li>
                </ul>
            </nav>
        </header>
        <div id="createGroup">
            <div class="loginprompt">
                <h1>Les creëren</h1>
            </div>
            <form method="POST" action="#">
                <div class="loginprompt">
                    <h4>Lesnaam</h4>
                </div>
                <div class="loginprompt">
                    <input type="text" id="lessonName" name="lessonName" placeholder="Mijn lesnaam..." required>
                </div>
                <div class="loginprompt">
                    <h4>Selecteer een groep</h4>
                </div>
                <div class="loginprompt">
                    <div>
                        <label for="groups">Kies een groep:</label>
                        <select name="groups" id="groups">
                        
                            <?php
                            try {
                                $dbHandler = new PDO ("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", "{$dbuser}", "{$dbpassword}");
                                
                            
                                $stmt = $dbHandler->prepare("SELECT `name` FROM `groups` WHERE `username` = :username");
                                $stmt->bindParam(":username", $username, PDO::PARAM_STR);
                                $stmt->execute();
                                $results = $stmt->fetchAll(PDO::FETCH_ASSOC);
                            
                                foreach ($results as $result) {
                                    echo "<option value='groupName' id='groupName'>$result[name]</option>";
                                }
                            } catch (PDOException $ex) {
                                $errors[] = $ex->getMessage();
                            }
                            ?>

                            
                        </select>
                    </div>
                </div>
                <div class="loginprompt">
                    <div>
                        <label for="subject">Kies een vak:</label>
                        <select name="subject" id="subject">
                        <option value="subject" id="subject">Aardrijkskunde</option>
                        <option value="subject" id="subject">Geschiedenis</option>
                        <option value="subject" id="subject">Biologie</option>
                        </select>
                    </div>
                </div>
                <div class="loginprompt">
                    <h4>Upload je Modellen</h4>
                </div>
                <div class="webeditor" style="text-align: center; padding: 0; border: 0; margin: 0">
                    <canvas id="unity-canvas" width=1200 height=800 style="width: 1200px; height: 800px; background: #FF8800"></canvas>
                    <script src="WebEditor/Build/1.1.0.loader.js"></script>
                    <script>
                        if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
                            // Mobile device style: fill the whole browser client area with the game canvas:
                            var meta = document.createElement('meta');
                            meta.name = 'viewport';
                            meta.content = 'width=device-width, height=device-height, initial-scale=1.0, user-scalable=no, shrink-to-fit=yes';
                            document.getElementsByTagName('head')[0].appendChild(meta);

                            var canvas = document.querySelector("#unity-canvas");
                            canvas.style.width = "50%";
                            canvas.style.height = "50%";
                            canvas.style.position = "fixed";

                            document.body.style.textAlign = "left";
                        }

                        createUnityInstance(document.querySelector("#unity-canvas"), {
                            dataUrl: "WebEditor/Build/f6e885ef6f053eabac2c1324ce768fac.data",
                            frameworkUrl: "WebEditor/Build/05262282d3d6f17fceaef00bac0bcb70.js",
                            codeUrl: "WebEditor/Build/350ddff92850a7af495559905791fa58.wasm",
                            streamingAssetsUrl: "WebEditor/StreamingAssets",
                            companyName: "WildInnovators",
                            productName: "WebEditor",
                            productVersion: "1.1.0",
                            // matchWebGLToCanvasSize: false, // Uncomment this to separately control WebGL canvas render size and DOM element size.
                            // devicePixelRatio: 1, // Uncomment this to override low DPI rendering on high DPI displays.
                        });
                    </script>
                </div>
                <br><br><br><br><br><br>
                <div class="loginprompt">
                    <input type="submit" id="createButton" name="createGroup" value="Creëer">
                </div>
            </form>
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
