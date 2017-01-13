
//При редактировании таблицы отмечает скрытые CheckBox, соответствующие редактируемой строке.
//Таким образом отслеживается изменения каких строк писать в лог

function SetRowAsChanged(currentRow) {
    var TargetCheckBoxName = "[" + currentRow + "].IsModified";
    var TargetCheckBoxID = "checkbox" + currentRow;

    var elem = document.getElementById(TargetCheckBoxID);    
    
    if (!elem.checked) {
        elem.setAttribute('checked', true);

        for (i = 0; i < document.getElementsByName(TargetCheckBoxName).length; i++) {            
            document.getElementsByName(TargetCheckBoxName)[i].setAttribute('value', "true");
        }
    }
}

