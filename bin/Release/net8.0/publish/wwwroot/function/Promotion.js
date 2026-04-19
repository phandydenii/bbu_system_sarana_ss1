class Promotion {
    constructor({
                    promotionId = null,
                    degreeId = null,
                    schoolId = null,
                    promotionNo = null,
                    academicYearStart = null,
                    academicYearEnd = null,
                    status = "",
                    graduateDate1 = null,
                    graduateDate2 = null
                } = {}) {
        this.promotionId = promotionId;
        this.degreeId = degreeId;
        this.schoolId = schoolId;
        this.promotionNo = promotionNo;
        this.academicYearStart = academicYearStart;
        this.academicYearEnd = academicYearEnd;
        this.status = status;
        this.graduateDate1 = graduateDate1 ? new Date(graduateDate1) : null;
        this.graduateDate2 = graduateDate2 ? new Date(graduateDate2) : null;
    }

    // Getters and setters
    get id() {
        return this.promotionId;
    }

    set id(value) {
        this.promotionId = value;
    }

    get degree() {
        return this.degreeId;
    }

    set degree(value) {
        this.degreeId = value;
    }

    get school() {
        return this.schoolId;
    }

    set school(value) {
        this.schoolId = value;
    }

    get number() {
        return this.promotionNo;
    }

    set number(value) {
        this.promotionNo = value;
    }

    get yearStart() {
        return this.academicYearStart;
    }

    set yearStart(value) {
        this.academicYearStart = value;
    }

    get yearEnd() {
        return this.academicYearEnd;
    }

    set yearEnd(value) {
        this.academicYearEnd = value;
    }

    get promotionStatus() {
        return this.status;
    }

    set promotionStatus(value) {
        this.status = value;
    }

    get gradDate1() {
        return this.graduateDate1;
    }

    set gradDate1(value) {
        this.graduateDate1 = value ? new Date(value) : null;
    }

    get gradDate2() {
        return this.graduateDate2;
    }

    set gradDate2(value) {
        this.graduateDate2 = value ? new Date(value) : null;
    }

    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new Promotion(item));
    }

    static async GetPromotions() {
        try {
            const response = await $.ajax({
                url: "/promotion/get-promotions",
                method: 'POST',
                data: {isAll: true}
            });
            if (response.status.code === "200" && response.data !== "") {
                return Promotion.fromApiArray(response.data);
            } else {
                ShowToastError(response.message);
            }
        } catch (err) {
            ShowToastError(err);
            return [];
        }
    }
}
