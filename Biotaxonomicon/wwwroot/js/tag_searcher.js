
let tagsSearch = document.getElementById('tagsSearchField');
tagsSearch.oninput = onTagsSearchFieldInput

let tagsMenu = document.getElementById('tagsDropDownMenu');
let tagsAddon = document.getElementById('tagsList');

let taglist = [];
let tagsIds = document.getElementById('tagsIdsInput');

function onTagMenuItemClick() {
    if (!taglist.includes(this.title)) {
        taglist.push(this.title);
        let str = '' + taglist[0];
        for (let i = 1; i < taglist.length; i++) {
            str += ' ' + taglist[i];
        }
        tagsIds.value = str;
        tagsAddon.append(this);
        this.className = 'badge badge-dark';
        this.onclick = function () {

            const index = taglist.indexOf(this.title);
            if (index > -1) {
                taglist.splice(index, 1);
            }
            let str = '';
            if (taglist.length > 0) {
                str = taglist[0];
                for (let i = 0; i < taglist.length; i++) {
                    str += ' ' + taglist[i];
                }
            }
            tagsIds.value = str;

            this.parentElement.removeChild(this);
        };
        console.log(taglist);
    }
    else {
        this.parentElement.removeChild(this);
    }
}
function tagsDropMenuDataFill(data) {
    console.log(data);
    while (tagsMenu.childElementCount > 0) {
        tagsMenu.removeChild(tagsMenu.firstChild);
    }
    for (let i = 0; i < data.length; i++) {

        let item = document.createElement('button');
        item.className = 'list-group-item list-group-item-action';
        item.type = "button"
        item.onclick = onTagMenuItemClick;
        item.title = data[i]['id'];
        item.innerHTML = data[i]['latinName'] + ',' + data[i]['russianName'];
        tagsMenu.appendChild(item);

    }
}
function beginSendXhr(Url ,onLoadCallback) {
    let xhr = new XMLHttpRequest();
    xhr.responseType = 'json';
    xhr.open('GET', Url);
    xhr.onload = function () {
        console.log('loaded: ' + xhr.response);
        onLoadCallback(xhr.response);
    }
    xhr.onerror = function () {
        console.log('error:' + xhr.status + ' ' + xhr.statusText)
    }
    xhr.readyState = function () {
        console.log('xhr ready state: ' + xhr.readyState);
    }
    xhr.send();
}
function onTagsSearchFieldInput() {
    beginSendXhr('https://' + location.host + '/Cabinet/Tag/FetchTags/?tagStr=' + tagsSearch.value,
        tagsDropMenuDataFill);
}