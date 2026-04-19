class Degree {
    constructor(data = {}) {
        this._degreeId = data.degreeId || 0;
        this._degreeName = data.degreeName || '';
        this._degreeNameKhmer = data.degreeInKhmer || '';
    }

    get degreeId() {
        return this._degreeId;
    }

    set degreeId(value) {
        this._degreeId = Number(value) || 0;
    }

    get degreeName() {
        return this._degreeName;
    }

    set degreeName(value) {
        this._degreeName = value?.trim() || 'Unknown';
    }

    get degreeInKhmer() {
        return this._degreeNameKhmer;
    }

    set degreeInKhmer(value) {
        this._degreeNameKhmer = value?.trim() || '';
    }

    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new Degree(item));
    }

    static async GetAllDegree() {
        try {
            const response = await $.ajax({
                url: "/degree/get-degrees",
                method: 'POST',
                data: {isAll: true}
            });

            if (response.status.code === "200" && response.data !== "") {
                return Degree.fromApiArray(response.data);
            } else {
                ShowToastError(response.message);
                return [];
            }
        } catch (err) {
            ShowToastError(err);
            return [];
        }
    }
}
