<?php
  require_once 'constants.php';

  $upload_json = filter_input(INPUT_POST, "json"); 

  try
  {
    $dbHandler = new PDO("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", $dbuser, $dbpassword); //Making connection with the database

    try
    {
      $stmt = $dbHandler->prepare("INSERT INTO models (`jsonfile`) VALUES (:json)"); //prepare a statement to use in the database
      $stmt->bindParam(":json", $upload_json, PDO::PARAM_STR); //Bind the variables 
      $stmt->execute();
    }
    catch (Exception $ex)
    {
      die($ex); //if there is something wrong with the connectie or something else, abport the mission. 
    }
  }
  catch (Exception $ex)
  {
    die($ex);
  }
?>