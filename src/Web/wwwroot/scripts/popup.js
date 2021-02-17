let popupList = [];

function showSuccessMessagePopup(text){
    showMessagePopup('success', text);
}

function showFailMessagePopup(text){
    showMessagePopup('fail', text);
}

function showMessagePopup(type, text){
    let pos = 0;
    const topOffset = 5;
    if(popupList.length !== 0){
        const last = popupList[popupList.length - 1];
        pos = last.getBoundingClientRect().top + last.getBoundingClientRect().height + topOffset;
    }
    
    const body = document.getElementsByTagName('nav').item(0);
    const popup = document.createElement('div');
 
    if(pos !== 0)
        popup.style.top = pos.toString() + 'px';
    
    body.appendChild(popup);
    popup.classList.add('popup', 'hidden');

    const title = document.createElement('div');
    popup.appendChild(title);
    title.classList.add('popup-title');
    
    const span = document.createElement('span');
    title.appendChild(span);
        
    switch (type) {
        case 'success':
            popup.classList.add('popup-success');
            title.appendChild(document.createTextNode(' Success'));
            span.classList.add('fa', 'fa-check-circle-o');
            break;
        case 'fail':
            popup.classList.add('popup-fail');
            title.appendChild(document.createTextNode(' Fail'));
            span.classList.add('fa', 'fa-times-circle-o');
            break;
    }

    const textElement = document.createElement('p');
    popup.appendChild(textElement);
    textElement.classList.add('popup-text');
    textElement.innerText = text;
    
    popupList.push(popup);
    
    setTimeout(() => {
        popup.classList.remove('hidden');
    }, 100)
    setTimeout(hidePopup, 2000);
}

function hidePopup(){
    const popup = popupList[0];
    if(popup == null)
        return;
    popup.classList.add('hidden');
    popupList.splice(0, 1);
    setTimeout(() => {
        popup.remove();
    }, 1000);
}
