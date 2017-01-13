function skm_LockScreen(str) {
    var lock = document.getElementById('skm_LockPane');
    if (lock)
        lock.className = 'LockOn';

    lock.innerHTML = str;
}



//    <p> 
//      <input type="submit" value="Click Me!" onclick="skm_LockScreen('We are processing your request...');" /> 
//   </p> 

//   <div id="skm_LockPane" class="LockOff"></div> 