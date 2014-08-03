<!DOCTYPE html>
<html>
    <head lang="en">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <link href="css/bootstrap.min.css" rel="stylesheet">
        <link href="css/custom.css" rel="stylesheet">
        <title>Sibyl System's Crawler</title>
    </head>
    
    <body>
        <div id="search_logo">
            <a href= "index.php" >
                <img src="image/sibyl.gif" alt="sibyl_logo" class="logo">
            </a>
        </div>
        
        <script>
        function checkUrl(s) {
            var regexp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/
            return regexp.test(s);
        }
        
        function isNormalInteger(str) {
            return /^\+?(0|[1-9]\d*)$/.test(str);
        }

        function validateForm()
        {
            var valid=true;
            var x=document.getElementById("crawl_link").value;
            var y=document.getElementById("crawl_depth").value;
            var a=document.getElementById("error_1");
            var b=document.getElementById("error_2");
            a.innerHTML="";
            b.innerHTML="";
            if (!checkUrl(x))
            {
                a.innerHTML = "URL not valid!";
                valid=false;
            }
            if (!isNormalInteger(y)){
                b.innerHTML = "Depth must be positive integer!";
                valid=false;
            }
            return valid;
        }
        </script>
        
        <div class="main_contain">
            <form class="form-horizontal" onsubmit="return validateForm()" method="post" action="crawl_execution.php">
                <div class="form-group">
                    <label class="col-sm-2 control-label" for="crawl_link">Start Link : </label>
                    <div class="col-sm-7">
                        <input class="form-control" type="text" name="crawl_link" id="crawl_link">
                    </div>
                    <span class="text-danger bg-danger" id="error_1"></span>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label" for="crawl_depth">Depth:</label>
                    <div class="col-sm-1">
                        <input class="form-control" type="text" name="crawl_depth" id="crawl_depth">
                    </div>
                    <span class="text-danger bg-danger" id="error_2"></span>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <input type="radio" name="crawl_type" id="crawl_dfs" value="dfs"><label for="crawl_dfs">DFS</label>
                    </div>
                    <div class="col-sm-offset-2 col-sm-10">
                        <input type="radio" name="crawl_type" id="crawl_bfs" value="bfs" checked><label for="crawl_bfs">BFS</label>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <input type="submit" class="btn btn-primary" name="submit" value="Crawl">
                    </div> 
                </div>
            </form>
        </div>
    </body>
</html>