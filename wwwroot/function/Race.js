class Race {
    constructor(data={}){
        this._raceId = data.raceId || 0;
        this._raceName = data.raceName || '';
        this._raceInKhmer = data.raceInKhmer || '';
    }
    get raceId(){
        return this._raceId;
    }
    set raceId(value){
        this._raceId = Number(value) || 0;
    }
    get raceName(){
        return this._raceName;
    }
    set raceName(value){
        this._raceName = value?.trim() || '';
    }
    get raceInKhmer(){
        return this._raceInKhmer;
    }
    set raceInKhmer(value){
        this._raceInKhmer = value?.trim() || '';
    }
    static async fromApiArray(jsonArray){
        return jsonArray.map(item => new Race(item));
    }
    static async GetAllRaces(){
        try {
            const response = await $.ajax({
                url:"/race/get-races",
                method:"POST",
                data:{isAll:true}
            });
            if(response.status.code === "200" && response.data !== ""){
                return Race.fromApiArray(response.data);
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