 class Province{
    constructor(data={}) {
        this._provinceId = data.provinceId || 0;
        this._provinceName = data.provinceName || '';
        this._provinceInKhmer = data.provinceInKhmer || '';
        this._isCity = data.isCity;
    }
    get provinceId(){
        return this._provinceId;
    }
    set provinceId(value){
        this._provinceId = Number(value) || 0;
    }
    get provinceName(){
        return this._provinceName;
    }
    set provinceName(value){
        this._provinceName = value?.trim() || '';
    }
    get provinceInKhmer(){
        return this._provinceInKhmer;
    }
    set provinceInKhmer(value){
        this._provinceInKhmer = value?.trim() || '';
    }
    get isCity(){
        return this._isCity;
    }
    set isCity(value){
        this._isCity = value;
    }
    static async fromApiArray(jsonArray){
        return jsonArray.map(item=> new Province(item));
    }
    
    static async GetAllProvinces(){
        try {
            const response = await $.ajax({
               url:"/Province/get-provinces",
               method:"POST",
               data:{isAll:true}, 
            });
            if(response.status === "200" && response.data !== ""){
                return Province.fromApiArray(response.data);
            }else{
                ShowToastError(response.message);
                return [];
            }
        }catch(err){
            ShowToastError(err.message);
            return [];
        }
    }
}