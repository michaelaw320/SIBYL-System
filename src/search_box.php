<script>
    function htmlspecialchars(str) {
        if (typeof(str) == "string") {
            str = str.replace(/&/g, "&amp;");
            str = str.replace(/"/g, "&quot;");
            str = str.replace(/'/g, "&#039;");
            str = str.replace(/</g, "&lt;");
            str = str.replace(/>/g, "&gt;");
        }
        return str;
    }
    function startSearch(){
        var x=document.getElementById("search_q");
        window.location.assign(location.pathname+"?q="+htmlspecialchars(x.value));
    }
    function enterSubmit(e){
        if (e.keyCode == 13 ||event.which == 13) {
            startSearch();
        }
    }
</script>
<div id="search_body">
    <div class="input-group">
        <input type="text" class="form-control" id="search_q" onkeypress="enterSubmit(event)" autofocus>
        <span class="input-group-btn">
        <button class="btn btn-primary" type="button" onclick="startSearch()"><span class="glyphicon glyphicon-search"></span></button>
        </span>
    </div>
</div>