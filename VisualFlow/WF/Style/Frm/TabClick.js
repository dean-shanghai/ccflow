function TabClick(id, url) {
    var iframesrc = document.getElementById('F' + id).src;
    if (iframesrc.indexOf('htm') > 0) {
        document.getElementById('F' + id).src = url;
    }
    return;
}