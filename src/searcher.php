
<?php
    echo '<div class="searchbox_bg">';
    echo '<a href="index.php"><img src="image/sibyl.gif" alt="sibyl_logo" class="small_logo"></a>';
    require("search_box.php");
    echo '</div>';
    
    set_time_limit(0);
    $query = htmlspecialchars_decode("SibylSearcher ".$_GET["q"]);

    exec($query,$ret);
    echo '<div class="main_contain">';
    if(count($ret)>1){
        if(isset($_GET['p']))
            $page_now=$_GET['p'];
        else
            $page_now=1;
        $found_num = count($ret)-1;
        $per_page = 10;
        $adjacent = 3;
        $max_pages = ceil($found_num/$per_page);
        echo '<p class="small_info">About '.$found_num.' result</p>';
        $start = ($page_now-1)*$per_page;
        $end = min($start+$per_page,$found_num);
        for($x=$start;$x<$end;$x++){
          $keywords = preg_split("/\[\^SEPARATOR&\]/",$ret[$x]);
          if(count($keywords)==2){
            if(strlen($keywords[0])>100){
                $keywords[0]=substr($keywords[0],0,97)."...";
            }
            if(strlen($keywords[1])>250){
                $keywords[1]=substr($keywords[1],0,247)."...";
            }
            echo '<a href="'.$keywords[1].'" class="result_title">'.$keywords[0].'</a>';
            echo '<br>';
            echo $keywords[1];
            echo '<br><br>';
          }
        }
        
        echo '<center>';
        echo '<ul class="pagination">';
        if($max_pages>1){
            if($page_now-1>0){
                echo '<li><a href="'.$_SERVER['PHP_SELF']."?q=".$_GET["q"].'&p='.($page_now-1).'">&laquo;</a></li>';
            }else{
                echo '<li class="disabled"><a>&laquo;</a></li>';
            }
            $start=max($page_now-$adjacent,1);
            $end=min($start+2*$adjacent,$max_pages);
            $start=max($end-2*$adjacent,1);
            for($i=$start;$i<=$end;$i++){
                echo '<li ';
                if($i==$page_now) echo 'class="active"';
                echo '><a href="'.$_SERVER['PHP_SELF']."?q=".$_GET["q"].'&p='.($i).'">'.($i).'</a></li>';
            }
            if($page_now+1<=$max_pages){
                echo '<li><a href="'.$_SERVER['PHP_SELF']."?q=".$_GET["q"].'&p='.($page_now+1).'">&raquo;</a></li>';
            }else{
                echo '<li class="disabled"><a>&raquo;</a></li>';
            }
        }
        echo '</ul>';
        echo '</center>';
        
    }else{
        echo "<p>Your search - <b>".$_GET["q"]."</b> - did not match any documents</p>";
        echo "<p>Suggestions:</p><ul>
        <li>Make sure that all words are spelled correctly.</li>
        <li>Try different keywords.</li>
        <li>Try more general keywords.</li>
        <li>Try fewer keywords.</li>
        </ul>";  
    }
    echo '</div>';
?>