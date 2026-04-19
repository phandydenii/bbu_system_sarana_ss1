class Term {
    constructor({
                    termId = null,
                    stageId = null,
                    termNo = null,
                    startDate = null,
                    endDate = null,
                    academicYearStart = null,
                    academicYearEnd = null,
                    status = "",
                    startPaymentDate = null
                } = {}) {
        this.termId = termId;
        this.stageId = stageId;
        this.termNo = termNo;
        this.startDate = startDate ? new Date(startDate) : null;
        this.endDate = endDate ? new Date(endDate) : null;
        this.academicYearStart = academicYearStart;
        this.academicYearEnd = academicYearEnd;
        this.status = status;
        this.startPaymentDate = startPaymentDate ? new Date(startPaymentDate) : null;
    }

    // --- Getters & Setters ---
    get id() {
        return this.termId;
    }

    set id(value) {
        this.termId = value;
    }

    get stage() {
        return this.stageId;
    }

    set stage(value) {
        this.stageId = value;
    }

    get number() {
        return this.termNo;
    }

    set number(value) {
        this.termNo = value;
    }

    get start() {
        return this.startDate;
    }

    set start(value) {
        this.startDate = value ? new Date(value) : null;
    }

    get end() {
        return this.endDate;
    }

    set end(value) {
        this.endDate = value ? new Date(value) : null;
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

    get termStatus() {
        return this.status;
    }

    set termStatus(value) {
        this.status = value;
    }

    get paymentStart() {
        return this.startPaymentDate;
    }

    set paymentStart(value) {
        this.startPaymentDate = value ? new Date(value) : null;
    }

    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new Term(item));
    }

    static async GetTerms() {
        try {
            const response = await $.ajax({
                url: "/term/get-terms",
                method: 'POST',
                data: {isAll: true}
            });
            if (response.status.code === "200" && response.data !== "") {
                return Term.fromApiArray(response.data);
            } else {
                ShowToastError(response.message);
            }
        } catch (err) {
            ShowToastError(err);
            return [];
        }
    }
}
