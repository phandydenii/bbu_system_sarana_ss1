class Faculty {
    constructor(data = {}) {
        this._facultyId = data.facultyId || 0;
        this._facultyName = data.facultyName || '';
        this._facultyNameInKhmer = data.facultyNameInKhmer || '';
    }

    // Getter and setter for universityId
    get facultyId() {
        return this._facultyId;
    }

    set facultyId(value) {
        this._facultyId = value;
    }

    // Getter and setter for universityName
    get facultyName() {
        return this._facultyName;
    }

    set facultyName(value) {
        this._facultyName = value;
    }

    // Getter and setter for universityNameInKhmer
    get facultyNameInKhmer() {
        return this._facultyNameInKhmer;
    }

    set facultyNameInKhmer(value) {
        this._facultyNameInKhmer = value;
    }

    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new Faculty(item));
    }
    static async GetFaulty() {
        try {
            const response = await $.ajax({
                url: "/Faculty/get-faculties",
                method: 'POST',
                data: {isAll: true}
            });
            if (response.status.code === "200" && response.data !== "") {
                return Faculty.fromApiArray(response.data);
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
