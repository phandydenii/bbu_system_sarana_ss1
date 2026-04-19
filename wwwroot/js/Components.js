class Components {
    static async BindSelectOptions(url, cbo, key, val) {
        try {
            const response = await $.ajax({
                url: url,
                method: 'POST',
                data: { isAll: true }
            });  
            if (response.status.code === "200" && response.data && response.data.length > 0) {
                const selectOptions = $(`#${cbo}`);
                selectOptions.empty();
                selectOptions.append("<option value='' disabled selected>Select</option>");
                response.data.forEach(item => {
                    selectOptions.append(`<option value='${item[key]}'>${item[val]}</option>`);
                }); 
                selectOptions.trigger("change"); 
            }else{
                console.log(response.responseText);
            }
        } catch (err) {
            console.log(err.responseText);
        }
    }
}
