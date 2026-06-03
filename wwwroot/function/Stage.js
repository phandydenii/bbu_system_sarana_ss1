class Stage {
    constructor({
                    stageId = null,
                    promotionId = null,
                    stageNo = null,
                    status = ""
                } = {}) {
        this.stageId = stageId;
        this.promotionId = promotionId;
        this.stageNo = stageNo;
        this.status = status;
    }

    // --- Getters & Setters ---

    get id() {
        return this.stageId;
    }

    set id(value) {
        this.stageId = value;
    }

    get promotion() {
        return this.promotionId;
    }

    set promotion(value) {
        this.promotionId = value;
    }

    get number() {
        return this.stageNo;
    }

    set number(value) {
        this.stageNo = value;
    }

    get stageStatus() {
        return this.status;
    }

    set stageStatus(value) {
        this.status = value;
    }

    // Mimic C# [NotMapped] property
    get tableName() {
        return "STAGE";
    }

    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new Stage(item));
    }


    static async GetStages({promotionId=0}) {
        try {
            const response = await $.ajax({
                url: "/stage/get-stages",
                method: 'POST',
                data: {
                    isAll: true,
                    promotionId
                }
            });
            if (response.status.code === "200" && response.data !== "") {
                return Stage.fromApiArray(response.data);
            } else {
                ShowToastError(response.message);
            }
        } catch (err) {
            ShowToastError(err);
            return [];
        }
    }

}


