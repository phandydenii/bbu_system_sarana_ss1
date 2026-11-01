class Nationality {
    constructor(data={}){
        this._nationalityId = data.nationalityId || 0;
        this._nationalityName = data.nationalityName || '';
        this._nationalityInKhmer = data.nationalityInKhmer || '';
        
    }
    get nationalityId(){
        return this._nationalityId;
    }
    set nationalityId(value){
        this._nationalityId = Number(value) || 0;
    }
    get nationalityName(){
        return this._nationalityName;
    }
    set nationalityName(value){
        this._nationalityName = value?.trim() || '';
    }
    get nationalityInKhmer(){
        return this._nationalityInKhmer;
    }
    set nationalityInKhmer(value){
        this._nationalityInKhmer = value?.trim() || '';
    }
    static async fromApiArray(jsonArray){
        return jsonArray.map(item => new Nationality(item));
    }
    static async GetAllNationality(){
        try {
            const response = await $.ajax({
                url:"/Nationality/get-nationalities",
                method:"POST",
                data:{isAll:true}
            });
            if(response.status.code === "200" && response.data !== ""){
                return Nationality.fromApiArray(response.data);
            }else{
                ShowToastError(response.message);
                return [];
            }
        }catch(err){
            ShowToastError(err);
            return [];
        }
    }
}