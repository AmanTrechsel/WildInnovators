<?php
  require_once 'constants.php';

  $upload_json = filter_input(INPUT_POST, "json"); 

  try
  {
    $dbHandler = new PDO("mysql:host={$dbhost};dbname={$dbname};charset=utf8;", $dbuser, $dbpassword); //Makes a connection with the database

    try
    {
      $stmt = $dbHandler->prepare("INSERT INTO models (`jsonfile`) VALUES (:json)"); //Prepare to insert the information/data into the database
      $stmt->bindParam(":json", $upload_json, PDO::PARAM_STR); //Binds the variable so the info can get put into the database
      $stmt->execute();
    }
    catch (Exception $ex)
    {
      die($ex); //if there is something wrong with the connection or the insertion into the database, abort the mission. 
    }
  }
  catch (Exception $ex)
  {
    die($ex);
  }
?>