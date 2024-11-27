<template>
  <h1>{{ title }}</h1>
  <ul
    class="Authors__wrapper"
    v-for="author in authors"
  >
    <li>
      <span>
        {{ author.name }}
      </span>
      <button
        class="Authors__button"
        @click.prevent="deleteHandler(author)"
      >
        Delete
      </button>
    </li>
  </ul>
</template>

<style>
.Authors__wrapper {
  display: flex;
  flex-wrap: nowrap;
  flex-direction: column;
  justify-content: space-evenly;
  align-items: left;
  height: 20vmin;
}

.Authors__button {
  background: none;
  border: none;
  text-decoration: underline;
  cursor: pointer;
}
</style>

<script>
export default {
  name: "Authors",
  data() {
    return {
      title: "All Authors",
      authors: [],
    }
  },
  methods: {
    async getData() {
      const authorsJson = await fetch(
        "http://localhost:5075/api/Library/GetAllAuthors"
      )
      const final = await authorsJson.json()
      this.authors = final.value
    },
    async deleteHandler(givenAuthor) {
      try {
        await fetch("http://localhost:5075/api/Library/DeleteAuthor", {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(givenAuthor),
        })
        this.getData()
      } catch (error) {
        console.error(error)
        throw error
      }
    },
  },
  mounted() {
    this.getData()
  },
}
</script>
