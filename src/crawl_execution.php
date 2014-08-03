<?php
    set_time_limit(0);
    $query="SibylSpider ".$_POST["crawl_type"]." ".$_POST["crawl_depth"]." ".$_POST["crawl_link"];
    exec($query,$ret);
    header("Location: crawler.php");
?>