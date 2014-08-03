<!DOCTYPE html>
<html>
    <head lang="en">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <link href="css/bootstrap.min.css" rel="stylesheet">
        <link href="css/custom.css" rel="stylesheet">
        <title>Sibyl System</title>
    </head>
    
    <body>
        <?php
            if(isset($_GET['q']))
                require("searcher.php");
            else{
                require("main_page.php");    
            }
        ?>
    </body>
</html>
