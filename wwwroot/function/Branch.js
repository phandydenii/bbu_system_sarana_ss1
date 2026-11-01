class Branch {
    constructor(data = {}){
        this._branchId = data.branchId || 0;
        this._branchName = data.branchName || '';
        this._branchNameInKhmer = data.branchNameInKhmer || '';
        this._ShortName = data.shortName || '';
        this._Address = data.address || '';
        this._Phone = data.phone || '';
    }
    get branchId(){
        return this._branchId;
    }
    set branchId(value){
        this._branchId = Number(value) || 0;
    }
    get branchName(){
        return this._branchName;
    }
    set branchName(value){
        this._branchName = value?.trim() || '';
    }
    get branchNameInKhmer(){
        return this._branchNameInKhmer;
    }
    set branchNameInKhmer(value){
        this._branchNameInKhmer = value?.trim() || '';
    }
    get shortName(){
        return this._ShortName;
    }
    set shortName(value){
        this._ShortName = value?.trim() || '';
    }
    get address(){
        return this._Address;
    }
    set address(value){
        this._Address = value?.trim() || '';
    }
    get phone(){
        return this._Phone;
    }
    set phone(value){
        this._Phone = value?.trim() || '';
    }
    static async fromApiArray(jsonArray){
        return jsonArray.map(item => new Branch(item));
    }
    static async GetAllBranch(){
        try {
            const response = await $.ajax({
                url:"/branch/get-branches",
                method:"POST",
                data:{isAll:true}
            });
            if(response.status.code === "200" && response.data !== ""){
                return Branch.fromApiArray(response.data);
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