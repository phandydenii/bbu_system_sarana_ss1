class Disability{
    constructor(data = {}){
        this._Id = data.id || 0;
        this._DisabilityName = data.disabilityName || '';
        this._DisabilityNameKh = data.disabilityNameKh || '';
    }
    get id(){
        return this._Id;
    }
    set id(value){
        this._Id = Number(value) || 0;
    }
    get disabilityName(){
        return this._DisabilityName;
    }
    set disabilityName(value){
        this._DisabilityName = value?.trim() || null;
    }
    get disabilityNameKh(){
        return this._DisabilityNameKh;
    }
    set disabilityNameKh(value){
        this._DisabilityNameKh = value?.trim() || null;
    }
    static async fromApiArray(jsonArray){
        return jsonArray.map(item => new Disability(item));
    }
    static async GetAllDisabilities(){
        try {
            const response = await $.ajax({
                url:"/disability/get-disabilities",
                method:"POST",
                data:{isAll:true}
            });
            if(response.status.code === "200" && response.data !== ""){
                return Disability.fromApiArray(response.data);
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