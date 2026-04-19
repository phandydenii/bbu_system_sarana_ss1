/**
 * Represents a student group, based on the provided C# class structure.
 * This class uses a flexible constructor that accepts an object with optional data.
 */
class StudentGroup {
    /**
     * Initializes a new instance of the StudentGroup class.
     * @param {object} data - An object containing student group data.
     * @param {number} [data.studentGroupId=0] - The unique ID of the student group.
     * @param {string} [data.studentId=''] - The ID of the student.
     * @param {number} [data.termNo=0] - The term number.
     * @param {number} [data.groupId=0] - The ID of the group.
     */
    constructor(data = {}) {
        this._studentGroupId = data.studentGroupId || 0;
        this._studentId = data.studentId || '';
        this._termNo = data.termNo || 0;
        this._groupId = data.groupId || 0;
    }

    // Getter and setter for studentGroupId
    get studentGroupId() {
        return this._studentGroupId;
    }

    set studentGroupId(value) {
        this._studentGroupId = value;
    }

    // Getter and setter for studentId
    get studentId() {
        return this._studentId;
    }

    set studentId(value) {
        this._studentId = value;
    }

    // Getter and setter for termNo
    get termNo() {
        return this._termNo;
    }

    set termNo(value) {
        this._termNo = value;
    }

    // Getter and setter for groupId
    get groupId() {
        return this._groupId;
    }

    set groupId(value) {
        this._groupId = value;
    }

    // Getter for the table name, which is not mapped to the database.
    get tableName() {
        return "STUDENT_GROUP";
    }

    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new StudentGroup(item));
    }
}
