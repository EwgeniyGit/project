function proverka(input) {    //запрет ввода текста в input
        input.value = input.value.replace(/[^\d]/g, '');
    };