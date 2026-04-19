class School {
    constructor({
                    schoolId = null,
                    schoolName = "",
                    schoolNameInKhmer = "",
                    schoolCode = "",
                    facultyId = null,
                    isFoundationSchool = null
                } = {}) {
        this.schoolId = schoolId;
        this.schoolName = schoolName;
        this.schoolNameInKhmer = schoolNameInKhmer;
        this.schoolCode = schoolCode;
        this.facultyId = facultyId;
        this.isFoundationSchool = isFoundationSchool;
    }

    // Getters and setters
    get id() {
        return this.schoolId;
    }

    set id(value) {
        this.schoolId = value;
    }

    get name() {
        return this.schoolName;
    }

    set name(value) {
        this.schoolName = value;
    }

    get nameKhmer() {
        return this.schoolNameInKhmer;
    }

    set nameKhmer(value) {
        this.schoolNameInKhmer = value;
    }

    get code() {
        return this.schoolCode;
    }

    set code(value) {
        this.schoolCode = value;
    }

    get faculty() {
        return this.facultyId;
    }

    set faculty(value) {
        this.facultyId = value;
    }

    get foundation() {
        return this.isFoundationSchool;
    }

    set foundation(value) {
        this.isFoundationSchool = value;
    }

    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new School(item));
    }

    static async GetSchool() {
        try {
            const response = await $.ajax({
                url: "/school/get-schools",
                method: 'POST',
                data: {isAll: true}
            });
            if (response.status.code === "200" && response.data !== "") {
                return School.fromApiArray(response.data);
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
